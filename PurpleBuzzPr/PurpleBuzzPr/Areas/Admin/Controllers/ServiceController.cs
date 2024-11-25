using Elfie.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.Models;
using System.Linq;

namespace PurpleBuzzPr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        IWebHostEnvironment _webHostEnvironment;
        public ServiceController(AppDbContext context, IWebHostEnvironment webHost)
        { 
            _context = context;
            _webHostEnvironment = webHost;
        }
        public async Task<IActionResult> Index()
        {
           IEnumerable<Service> services = await _context.Services.Include(s=>s.Works).ToListAsync();

           return View(services);
        }

       
        public IActionResult Delete(int Id)
        {
            Service? deletedService = _context.Services.Find(Id);
            if (deletedService == null) { return NotFound(); }
            else { 
                _context.Services.Remove(deletedService); 
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {
            
            if (!ModelState.IsValid)
            {
                return View(service);
            }
            if (!service.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Only image format accepted");
                return View(service);
            }
            string path = _webHostEnvironment.WebRootPath + @"\Upload\ServiceImages\";
            string fileName = service.Image.FileName;
            using(FileStream fileStream = new FileStream(path + fileName, FileMode.Create))
            {
                service.Image.CopyTo(fileStream);
            }

            service.MainImageUrl = fileName;

            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? Id)
        {
            Service? service = _context.Services.Find(Id);
            if (service is null)
            {
                return NotFound("No such service");
            }
            return View(nameof(Create),service);
        }

        [HttpPost]
        public IActionResult Update(Service service)
        {
            Service? updatedService = _context.Services.AsNoTracking()
                .FirstOrDefault(x => x.Id == service.Id);

            if (updatedService is null)
            {
                return NotFound("No such service");
            }
            
            _context.Services.Update(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
