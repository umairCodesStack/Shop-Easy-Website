using Application;
using Domain.Entities;
using Ecommere_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Ecommere_Website.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductService _productService;
        public CartController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public IActionResult AddToCart(int MyId)
        {
            Product product = _productService.GetProductById(MyId);

            ProductViewModel prod = new ProductViewModel
            {
                Id = MyId,
                Name = product.Name,
                UnitPrice = product.Price,
                quantity = 1,
                StockQuantity = product.StockQuantity,
                ImageUrls = product.ImageUrls.Select(i => i.ImageUrl).ToList()
            };
            prod.TotalPrice = prod.UnitPrice * prod.quantity;

            List<ProductViewModel> lis = new List<ProductViewModel>();

            if (HttpContext.Session.Keys.Contains("ProductId"))
            {
                string listOfItems = HttpContext.Session.GetString("ProductId");
                lis = JsonSerializer.Deserialize<List<ProductViewModel>>(listOfItems);
            }

            lis.Add(prod);

            string updatedItems = JsonSerializer.Serialize(lis);
            HttpContext.Session.SetString("ProductId", updatedItems);

            return RedirectToAction("HomePage","Home");
        }
        public IActionResult IncrementItemInCart(int PlusId)
        {
            List<ProductViewModel> lis = new List<ProductViewModel>();

            if (HttpContext.Session.Keys.Contains("ProductId"))
            {
                string listOfItems = HttpContext.Session.GetString("ProductId");
                lis = JsonSerializer.Deserialize<List<ProductViewModel>>(listOfItems);
            }

            var item = lis.FirstOrDefault(p => p.Id == PlusId);
            if (item != null)
            {
                item.quantity++;
                item.TotalPrice = item.UnitPrice * item.quantity; 
            }

            string updatedItems = JsonSerializer.Serialize(lis);
            HttpContext.Session.SetString("ProductId", updatedItems);
            return RedirectToAction("Cart");

        }
        public IActionResult DecrementItemInCart(int Id)
        {
            List<ProductViewModel> lis = new List<ProductViewModel>();

            if (HttpContext.Session.Keys.Contains("ProductId"))
            {
                string listOfItems = HttpContext.Session.GetString("ProductId");
                lis = JsonSerializer.Deserialize<List<ProductViewModel>>(listOfItems);
            }

            var item = lis.FirstOrDefault(p => p.Id == Id);
            if (item != null)
            {
                if (item.quantity > 0) 
                {
                    item.quantity--;
                    item.TotalPrice = item.UnitPrice * item.quantity;
                }
                 
            }

            string updatedItems = JsonSerializer.Serialize(lis);
            HttpContext.Session.SetString("ProductId", updatedItems);
            return RedirectToAction("Cart");

        }
        public IActionResult RemoveFromCart(int MyId)
        {
            List<ProductViewModel> lis = new List<ProductViewModel>();

            if (HttpContext.Session.Keys.Contains("ProductId"))
            {
                string listOfItems = HttpContext.Session.GetString("ProductId");
                lis = JsonSerializer.Deserialize<List<ProductViewModel>>(listOfItems);
            }
            ProductViewModel remove = new ProductViewModel();
            foreach (var temp in lis)
            {
                if (temp.Id == MyId)
                {
                    remove = temp;
                }
            }
            lis.Remove(remove);
            string updatedItems = JsonSerializer.Serialize(lis);
            HttpContext.Session.SetString("ProductId", updatedItems);
            return RedirectToAction("Cart");
        }
        public IActionResult Cart()
        {
            List<ProductViewModel> lis = new List<ProductViewModel>();
            if (HttpContext.Session.Keys.Contains("ProductId"))
            {
                string listOfItems = HttpContext.Session.GetString("ProductId");
                lis = JsonSerializer.Deserialize<List<ProductViewModel>>(listOfItems);
            }
            return View(lis);
        }

    }
}
