using Microsoft.AspNetCore.Mvc;
using System.Collections;
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

            IEnumerable<User> userler = new List<User>(users);
            

            return View();
        }
        public IActionResult About()
        {


            return View();
        }

    }
}
