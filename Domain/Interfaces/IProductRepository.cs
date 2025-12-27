using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        public Product AddProduct(AddProductDTO product);

        public void UpdateProduct(int productId);
        public void DeleteProduct(int productId);
        public Product GetProductById(int id);
        public List<GetProductDTO> GetAllProducts();
        public List<Product> SearchProductByName(string name);
        public List<Product> SearchProductByCatagorey(string catagorey);

    }
}
