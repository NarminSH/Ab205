using Microsoft.AspNetCore.Mvc;

namespace PurpleBuzzPr.Controllers;

public class WorkController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    // I have a question
    public IActionResult SingleWork(int id)
    {
        return View();
    }
    // I have a question
}
