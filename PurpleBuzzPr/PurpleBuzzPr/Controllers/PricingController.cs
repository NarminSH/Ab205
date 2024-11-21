using Microsoft.AspNetCore.Mvc;

namespace PurpleBuzzPr.Controllers;

public class PricingController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
