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

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name is required", nameof(product.Name));

            if (product.Price <= 0)
                throw new ArgumentException("Product price must be greater than zero", nameof(product.Price));

            // Optional: validate Store exists
            var storeExists = _context.Stores.Any(s => s.Id == product.StoreId);
            if (!storeExists)
                throw new ArgumentException("Invalid StoreId");

            Product newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description ?? string.Empty,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                StoreId = product.StoreId,
                userId = product.userId,
                Category = product.Category,
                discount = product.discount,
                tag = product.tag,

                Rating = 0,
                Sizes = product.Sizes?
                    .Select(size => new ProductSize { Size = size })
                    .ToList() ?? new List<ProductSize>(),

                Colors = product.Colors?
                    .Select(color => new ProductColor { Color = color })
                    .ToList() ?? new List<ProductColor>(),

                ImageUrls = product.ImageUrls?
                    .Select(url => new ProductImage { ImageUrl = url })
                    .ToList() ?? new List<ProductImage>()
            };

            _context.Products.Add(newProduct);
            _context.SaveChanges();

            return newProduct;
        }


        public int DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                return _context.SaveChanges();
            }
            return 0; // No product found to delete
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
                    OriginalPrice = p.Price,
                    ImageUrl = p.ImageUrls.Select(img => img.ImageUrl).FirstOrDefault(),
                    StoreLogoUrl = p.Store.LogoUrl,
                    StoreName = p.Store.Name,
                    Discount = p.discount,
                    FinalPrice = p.discount.HasValue ? p.Price * (decimal)(1 - p.discount.Value / 100) : p.Price,
                    ReviewsCount = p.Reviews.Count,
                    Rating = p.Rating,
                    Tag = p.tag,
                    StockQuantity = p.StockQuantity,
                    StoreId = p.StoreId,
                    VendorId = p.userId,


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
        public List<GetProductDetailDTO> getProductByUserId(int userId)
        {
            var products = _context.Products
                .Where(p => p.userId == userId)
                .Include(p => p.ImageUrls)
                .Select(p => new GetProductDetailDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Category = p.Category,
                    OriginalPrice = p.Price,
                    ImageUrls = p.ImageUrls.Select(img => img.ImageUrl).ToList(),
                    StoreLogoUrl = p.Store.LogoUrl,
                    StoreName = p.Store.Name,
                    Discount = p.discount,
                    FinalPrice = p.discount.HasValue ? p.Price * (decimal)(1 - p.discount.Value / 100) : p.Price,
                    ReviewsCount = p.Reviews.Count,
                    Rating = p.Rating,
                    Tag = p.tag,
                    StockQuantity = p.StockQuantity,
                    StoreId = p.StoreId,
                    VendorId = userId,
                })
                .ToList();
            return products;
        }
        public GetProductDetailDTO GetProductById(int Id)
        {
            var product = _context.Products
                 .Include(p => p.ImageUrls)
                 .Select(p => new GetProductDetailDTO
                 {
                     Id = p.Id,
                     Name = p.Name,
                     Description = p.Description,
                     Category = p.Category,
                     OriginalPrice = p.Price,
                     ImageUrls = p.ImageUrls.Select(img => img.ImageUrl).ToList(),
                     StoreLogoUrl = p.Store.LogoUrl,
                     StoreName = p.Store.Name,
                     Discount = p.discount,
                     FinalPrice = p.discount.HasValue ? p.Price * (decimal)(1 - p.discount.Value / 100) : p.Price,
                     ReviewsCount = p.Reviews.Count,
                     Rating = p.Rating,
                     Tag = p.tag,
                     StockQuantity = p.StockQuantity,
                     Sizes = p.Sizes.Select(s => s.Size).ToList(),
                     Colors = p.Colors.Select(c => c.Color).ToList(),
                     StoreId = p.StoreId,
                     VendorId = p.userId,

                 })
                 .FirstOrDefault(p => p.Id == Id);
            return product;
        }

        public bool UpdateProduct(int productId, UpdateProductDTO update)
        {
            try
            {
                // 1. Find the existing product
                var product = _context.Products
                    .Include(p => p.Sizes)
                    .Include(p => p.Colors)
                    .Include(p => p.ImageUrls)
                    .FirstOrDefault(p => p.Id == productId);

                if (product == null)
                {
                    return false; // Product not found
                }

                // 2. Update basic product fields
                if (update.Name != null)
                    product.Name = update.Name;
                if (update.Description != null)
                    product.Description = update.Description;
                if (update.Price != null)
                    product.Price = update.Price ?? product.Price;
                if (update.StockQuantity != null)
                    product.StockQuantity = update.StockQuantity ?? product.StockQuantity;
                if (update.Category != null)
                    product.Category = update.Category;

                if (update.discount != null)
                    product.discount = update.discount ?? 0;
                if (update.tag != null)
                    product.tag = update.tag;


                // Calculate final price



                // 3. Handle Sizes
                // Remove sizes
                if (update.SizesToRemove != null && update.SizesToRemove.Any())
                {
                    var sizesToDelete = product.Sizes
                        .Where(ps => update.SizesToRemove.Contains(ps.Size))
                        .ToList();

                    _context.ProductSizes.RemoveRange(sizesToDelete);
                }

                // Add new sizes
                if (update.NewSizes != null && update.NewSizes.Any())
                {
                    foreach (var size in update.NewSizes)
                    {
                        // Check if size already exists (to prevent duplicates)
                        if (!product.Sizes.Any(ps => ps.Size == size))
                        {
                            product.Sizes.Add(new ProductSize
                            {
                                ProductId = productId,
                                Size = size
                            });
                        }
                    }
                }

                // 4. Handle Colors
                // Remove colors
                if (update.ColorsToRemove != null && update.ColorsToRemove.Any())
                {
                    var colorsToDelete = product.Colors
                        .Where(pc => update.ColorsToRemove.Contains(pc.Color))
                        .ToList();

                    _context.ProductColors.RemoveRange(colorsToDelete);
                }

                // Add new colors
                if (update.NewColors != null && update.NewColors.Any())
                {
                    foreach (var color in update.NewColors)
                    {
                        // Check if color already exists (to prevent duplicates)
                        if (!product.Colors.Any(pc => pc.Color == color))
                        {
                            product.Colors.Add(new ProductColor
                            {
                                ProductId = productId,
                                Color = color
                            });
                        }
                    }
                }

                // 5. Handle Images
                // Remove images
                if (update.ImageUrlsToRemove != null && update.ImageUrlsToRemove.Any())
                {
                    var imagesToDelete = product.ImageUrls
                        .Where(pi => update.ImageUrlsToRemove.Contains(pi.ImageUrl))
                        .ToList();

                    _context.ProductImages.RemoveRange(imagesToDelete);
                }

                // Add new images
                if (update.NewImageUrls != null && update.NewImageUrls.Any())
                {
                    foreach (var imageUrl in update.NewImageUrls)
                    {
                        // Check if image already exists (to prevent duplicates)
                        if (!product.ImageUrls.Any(pi => pi.ImageUrl == imageUrl))
                        {
                            product.ImageUrls.Add(new ProductImage
                            {
                                ProductId = productId,
                                ImageUrl = imageUrl
                            });
                        }
                    }
                }

                // 6. Update the product and save changes
                _context.Products.Update(product);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating product: {ex.Message}");
                return false;
            }
        }
        public List<string> GetCatagories()
        {
            var categories = _context.Products
                .Select(p => p.Category)
                .Distinct()
                .ToList();
            return categories;
        }
    }
}
