using Microsoft.AspNetCore.Mvc;

namespace PurpleBuzzPr.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
