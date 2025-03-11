using Microsoft.AspNetCore.Mvc;

namespace Ecommere_Website.Controllers
{
    public class ProductController : Controller
    {
        
        public IActionResult GetAllProducts()
        {
            return View();
        }
    }
}
