using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Application
{
    public class ProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo) 
        {
            _repo = repo;
        }
        public void AddProduct(Product product) 
        {
            _repo.AddProduct(product);
        }

        public void UpdateProduct(int productId) 
        {
            _repo.UpdateProduct(productId);
        }
        public void DeleteProduct(int productId) 
        {
            _repo.DeleteProduct(productId);
        }
        public Product GetProductById(int id) 
        {
            return _repo.GetProductById(id);
        }
        public List<Product> GetAllProducts() 
        {
            return _repo.GetAllProducts();
        }
        public List<Product> SearchProductByName(string name) 
        {
            return _repo.SearchProductByName(name);
        }
        public List<Product> SearchProductByCatagorey(string catagorey) 
        {
            return _repo.SearchProductByCatagorey(catagorey);
        }
    }
}
