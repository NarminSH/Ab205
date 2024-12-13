using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.Models;
using PurpleBuzzPr.Utilities;
using PurpleBuzzPr.ViewModels.Home;
using System.Net;

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
        //var username = Request.Cookies["UserName"];
        //var username = HttpContext.Session.GetString("SessionUserName");
        //if (username == null)
        //{
        //    return BadRequest("username nulldir");
        //}
        IEnumerable<Service> services = _context.Services.ToList();
        HomeVM homeVM = new HomeVM()
        {
            Services = services,
        };
        
        
        return View(homeVM);
    }

    public IActionResult Details()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
    public IActionResult GlobalException()
    {
        return View();
    }
    
}
