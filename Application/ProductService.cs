using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
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
        public Product AddProduct(AddProductDTO product)
        {
            return _repo.AddProduct(product);
        }

        public bool UpdateProduct(int productId, UpdateProductDTO update)
        {
            return _repo.UpdateProduct(productId, update);
        }
        public int DeleteProduct(int productId)
        {
            return _repo.DeleteProduct(productId);
        }
        public GetProductDetailDTO GetProductById(int id)
        {
            return _repo.GetProductById(id);
        }
        public List<GetProductDTO> GetAllProducts()
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
        public List<string> GetCatagories()
        {
            return _repo.GetCatagories();
        }
        public List<GetProductDetailDTO> getProductByUserId(int userId)
        {
            return _repo.getProductByUserId(userId);
        }
    }
}
