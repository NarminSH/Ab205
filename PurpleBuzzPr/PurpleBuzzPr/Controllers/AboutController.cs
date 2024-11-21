using Microsoft.AspNetCore.Mvc;

namespace PurpleBuzzPr.Controllers;

public class AboutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
