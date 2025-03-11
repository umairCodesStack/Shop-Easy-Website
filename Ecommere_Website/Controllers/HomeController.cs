using System.Diagnostics;
using Ecommere_Website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ecommere_Website.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("LogIn", "Account");
        }
        public IActionResult HomePage() 
        {
            var products = _context.Products
           .Include(p => p.ImageUrls) 
            .ToList();
            return View("Index",products);
        }
        public IActionResult  SearchProduct(string searchTerm) 
        {
            List<Product> products = new List<Product>();
            products = _context.Products
           .Include(p => p.ImageUrls)
           .Where(p => p.Name.Contains(searchTerm))
           .ToList();
            return View("Index",products);
        }
        public IActionResult ShowByCatagorey(string catagorey) 
        {
            List<Product> products = new List<Product>();
            products = _context.Products
           .Include(p => p.ImageUrls)
           .Where(p => p.Category.Contains(catagorey))
           .ToList();
            return View("Index", products);
            
        }
        public IActionResult ProductDetails(int ProductId) 
        {
            Product product= _context.Products.Include(p=>p.ImageUrls).FirstOrDefault(p=>p.Id == ProductId);
            return View(product);
        }
        public IActionResult ApplyFilter() 
        {
            return View();
        }    
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
