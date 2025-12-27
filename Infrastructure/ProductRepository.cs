using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Product AddProduct(AddProductDTO product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            // Validate required fields
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name is required", nameof(product.Name));

            if (product.Price <= 0)
                throw new ArgumentException("Product price must be greater than zero", nameof(product.Price));

            Product newproduct = new Product
            {
                Name = product.Name,
                Description = product.Description ?? string.Empty,
                Price = product.Price,
                Category = product.Category ?? string.Empty,
                Rating = product.Rating,
                StockQuantity = product.StockQuantity,
                userId = product.UserId,
                // Safe null-coalescing
                Sizes = product.Sizes?.Select(size => new ProductSize { Size = size }).ToList(),
                Colors = product.Colors?.Select(color => new ProductColor { Color = color }).ToList(),
                         
                ImageUrls = product.ImageUrls?.Select(url => new ProductImage { ImageUrl = url }).ToList()
                            
            };

            _context.Products.Add(newproduct);
            _context.SaveChanges();

            return newproduct; // Return the created product
        }

        public void DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }


        public List<GetProductDTO> GetAllProducts()
        {
            var products = _context.Products
                .Include(p => p.ImageUrls)
                .Select(p => new GetProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Category = p.Category,
                    StockQuantity = p.StockQuantity,
                    Price = p.Price,
                    Rating = p.Rating,
                    ImageUrls = p.ImageUrls.Select(img => img.ImageUrl).ToList()
                })
                .ToList();
            return products;
        }
        public List<Product> SearchProductByName(string name) 
        {
            List<Product> products = new List<Product>();
            products = _context.Products
           .Include(p => p.ImageUrls)
           .Where(p => p.Name.Contains(name))
           .ToList();
            return products;
        }
        public List<Product> SearchProductByCatagorey(string catagorey) 
        {
            List<Product> products = new List<Product>();
            products = _context.Products
           .Include(p => p.ImageUrls)
           .Where(p => p.Category.Contains(catagorey))
           .ToList();
            return products;
        }
        public Product GetProductById(int Id)
        {
            Product product = _context.Products.Include(p => p.ImageUrls).FirstOrDefault(p => p.Id==Id);
            return product;
        }

        public void UpdateProduct(int productId)
        {
            _context.Update(productId);
            _context.SaveChanges();
        }
    }
}
