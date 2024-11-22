using Microsoft.AspNetCore.Mvc;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.Models;
using PurpleBuzzPr.ViewModels.Home;

namespace PurpleBuzzPr.Controllers;

public class HomeController : Controller
{
    readonly AppDbContext _context;
    public HomeController(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    
    public IActionResult Index()
    {
        IEnumerable<Service> services = _context.Services.ToList();
        IEnumerable<Work> works = _context.Works.OrderByDescending(w=>w.Id).Take(3).ToList();
      
        HomeVM homeVM = new HomeVM()
        {
            Services = services,
            Works = works
        };

        return View(homeVM);
    }

    public IActionResult Details()
    {
       

        return View();
    }
}
