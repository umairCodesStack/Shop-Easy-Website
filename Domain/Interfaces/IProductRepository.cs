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

        public bool UpdateProduct(int productId, UpdateProductDTO update);
        public int DeleteProduct(int productId);
        public GetProductDetailDTO GetProductById(int id);
        public List<GetProductDTO> GetAllProducts();
        public List<Product> SearchProductByName(string name);
        public List<Product> SearchProductByCatagorey(string catagorey);
        public List<string> GetCatagories();
        public List<GetProductDetailDTO> getProductByUserId(int userId);

    }
}
