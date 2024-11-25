using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.Models;

namespace PurpleBuzzPr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorksController : Controller
    {
        private readonly AppDbContext _context;

        public WorksController(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Works.ToListAsync();
            return View(await appDbContext);
        }

        
        
        public IActionResult Create()
        {
            ViewBag.Services = _context.Services;
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(Work work)
        {
            if (ModelState.IsValid)
            {
                Service? service = _context.Services.Find(work.ServiceId);
                if (service != null)
                {
                    _context.Works.Add(work);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError("ServiceId", "Something went wrong");
            ViewBag.Services = _context.Services;

            return View(work);
        }
    }
}
