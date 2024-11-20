using Microsoft.AspNetCore.Mvc;
using ZonApp.Models;

namespace ZonApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            Product product = new Product() { Id = 1, Name ="Vertu Watch black", Price=8000, MainImageUrl="images/coff.png" };
            Product product1 = new Product() { Id = 2, Name ="Apple Watch black", Price=1000, MainImageUrl="images/blog1.jpg" };
            ICollection<Product> products = new List<Product>() { product, product1 };
 
            return View(products);
        }

        public IActionResult Detail(int Id)
        {
            Product product = new Product() { Id = 1, Name = "Vertu Watch black", Price = 8000, MainImageUrl = "images/coff.png" };
            Product product1 = new Product() { Id = 2, Name = "Apple Watch black", Price = 1000, MainImageUrl = "images/blog1.jpg" };
            ICollection<Product> products = new List<Product>() { product, product1 };
            Product? dbProduct = products.FirstOrDefault(p=> p.Id == Id);
            return View(dbProduct);

        }
    }
}
