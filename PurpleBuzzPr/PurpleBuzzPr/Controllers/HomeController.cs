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
        
        Work work1 = new Work()
        {
            Id = 1,
            MainImageUrl = "recent-work-01.jpg",
            Title = "Social Media",
            Description = "loremnsdjfhsfhsf"
        };
        Work work2 = new Work()
        {
            Id = 2,
            MainImageUrl = "recent-work-02.jpg",
            Title = "Social kcjkshfksjhfskfhd",
            Description = "kfjskdfjkljkejreiojgeiorj"
        };
        IEnumerable<Work> works = new List<Work>() { work1, work2 };
        IEnumerable<Service> services = _context.Services.ToList();
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
