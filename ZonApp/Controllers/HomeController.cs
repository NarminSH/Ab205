using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ZonApp.Models;

namespace ZonApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           List<User> users = new List<User> 
           { 
               new User() { Id = 1, Name = "Fizi" },
               new User() { Id = 2, Name = "Togrul" } 
           };
            
            //ViewBag.users = users;
            ViewData["users"] ="null";
            //TempData["message"] = "hello from temp data";
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
