using Microsoft.AspNetCore.Mvc;

namespace PurpleBuzzPr.Controllers;

public class SingleWorkController : Controller
{
    public IActionResult Index(int id)
    {
        return View();
    }
}
