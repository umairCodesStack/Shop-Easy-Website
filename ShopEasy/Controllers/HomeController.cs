using System.Diagnostics;
using Application;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;

namespace ShopEasy.Controllers
{
    public class HomeController : Controller
    {

        private readonly ProductService productService;
        public HomeController(ProductService productService)
        {
            this.productService = productService;
        }
        
    
        public IActionResult Index()
        {
            return RedirectToAction("LogIn", "Account");
        }
        public IActionResult HomePage()
        {
            var products = productService.GetAllProducts();
            return View("Index", products);
        }
        public IActionResult SearchProduct(string searchTerm)
        {
            List<Product> products = new List<Product>();
           
            products=productService.SearchProductByName(searchTerm);
            return View("Index", products);
        }
        public IActionResult ShowByCatagorey(string catagorey)
        {
            List<Product> products = new List<Product>();
            products = productService.SearchProductByCatagorey(catagorey);
            return View("Index", products);

        }
        public IActionResult ProductDetails(int ProductId)
        {
            Product product = productService.GetProductById(ProductId);
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
