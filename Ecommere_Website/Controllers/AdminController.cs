using Microsoft.AspNetCore.Mvc;

namespace Ecommere_Website.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
