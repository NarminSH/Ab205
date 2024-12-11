using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.DTOs.WorkDTOs;
using PurpleBuzzPr.Models;
using PurpleBuzzPr.Utilities;

namespace PurpleBuzzPr.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WorksController : Controller
    {
        private readonly AppDbContext _context;
        IWebHostEnvironment _webHostEnvironment;
        public WorksController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Index()
        {
            var appDbContext = await _context.Works.Include(w=> w.EmployeeWorks).ThenInclude(w=> w.Employee).ToListAsync();
            
            return View(appDbContext);
        }


        
        public IActionResult Create()
        {
            ViewBag.Services = _context.Services;
            ViewBag.Employees = new SelectList(_context.Employees, "Id", "Name" );
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkDTO createWork)
        {
            if (ModelState.IsValid)
            {
                Service? service = _context.Services.Find(createWork.ServiceId);
                foreach (int employeeId in createWork.EmployeeIds)
                {
                    if (!_context.Employees.Any(e => e.Id == employeeId))
                    {
                        return NotFound();
                    }
                }
                if (service != null)
                {
                    Work newWork = new Work();
                    newWork.Title = createWork.Title;
                    newWork.Description = createWork.Description;
                    newWork.ServiceId = service.Id;
                    newWork.MainImageUrl = createWork.MainImageUrl.Upload(_webHostEnvironment.WebRootPath, @"\Upload\WorkImages\");
                    _context.Works.Add(newWork);
                    List<WorkPhotos> workPhotos = new List<WorkPhotos>();
                    foreach (IFormFile item in createWork.Images ?? new List<IFormFile>())
                    {
                        string imageUrl = item.Upload(_webHostEnvironment.WebRootPath, @"\Upload\WorkImages\");
                        WorkPhotos workPhoto = new WorkPhotos()
                        {
                            Work = newWork,
                            ImageUrl = imageUrl
                        };
                        workPhotos.Add(workPhoto);
                    }
                    _context.WorkPhotos.AddRange(workPhotos);
                    //var result = _context.Employees.Any(e => createWork.EmployeeIds.Contains(e.Id));
                    foreach (var employeeId in createWork.EmployeeIds)
                    {
                        _context.EmployeeWorks.Add(new EmployeeWork
                        {
                            EmployeeId = employeeId,
                            Work =newWork
                        });
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError("ServiceId", "Something went wrong");
            ViewBag.Services = _context.Services;
            ViewBag.Employees = new SelectList(_context.Employees, "Id", "Name");
            return View(createWork);
        }

        public IActionResult Update(int Id)
        {
            Work? work = _context.Works.Include(s => s.Photos).FirstOrDefault(s => s.Id == Id);
            if (work is null)
            {
                return RedirectToAction(nameof(Index), nameof(Work));
            }
            UpdateWorkDTO updateWorkDTO = new UpdateWorkDTO()
            {
                Title = work.Title,
                Description = work.Description,
                ExistingMainImageUrl = work.MainImageUrl,
                ExistingPhotos = work.Photos,

            };
            return View(updateWorkDTO);
        }
        [HttpPost]
        public IActionResult Update(UpdateWorkDTO updateWorkDTO)
        {
            if (ModelState.IsValid)
            {
                Work? existingWork = _context.Works.Find(updateWorkDTO.Id);
                if (existingWork is null)
                {
                    return BadRequest();
                }
                if (updateWorkDTO.NewPhotos != null && updateWorkDTO.NewPhotos.Count>0) 
                {
                    foreach (IFormFile photo in updateWorkDTO.NewPhotos)
                    {
                        if (!photo.CheckType())
                        {
                            ModelState.AddModelError("NewPhotos", "Only Image accepted");
                            return View();
                        }
                        if (!photo.CheckSize(2))
                        {
                            ModelState.AddModelError("NewPhotos", "No more than 2 mb is accepted");
                            return View();
                        }
                    }
                    List<WorkPhotos> updatedPhotos = new List<WorkPhotos>();
                    foreach (IFormFile item in updateWorkDTO.NewPhotos)
                    {
                        string imageUrl = item.Upload(_webHostEnvironment.WebRootPath, @"\Upload\WorkImages\");
                        WorkPhotos workPhoto = new WorkPhotos()
                        {
                            Work = existingWork,
                            ImageUrl = imageUrl
                        };
                        updatedPhotos.Add(workPhoto);
                    }
                    _context.WorkPhotos.AddRange(updatedPhotos);
                }
                existingWork.Title = updateWorkDTO.Title;
                existingWork.Description = updateWorkDTO.Description;
                existingWork.MainImageUrl = updateWorkDTO.NewMainImage.Upload(_webHostEnvironment.WebRootPath, @"\Upload\WorkImages\");
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(updateWorkDTO);
        }



        public IActionResult DeleteWorkPhoto(int photoId)
        {
            WorkPhotos? photoToDeleted = _context.WorkPhotos.Include(w=>w.Work).FirstOrDefault(w=>w.Id == photoId);
            if (photoToDeleted is null)
            {
                return RedirectToAction(nameof(Index));
            }
            System.IO.File.Delete(_webHostEnvironment.WebRootPath + @$"\Upload\WorkImages\{photoToDeleted.ImageUrl}");
            _context.WorkPhotos.Remove(photoToDeleted);
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Update), new {photoToDeleted.Work.Id});
        }
    }
}
