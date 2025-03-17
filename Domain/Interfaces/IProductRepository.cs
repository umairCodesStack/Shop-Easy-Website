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
        public void AddProduct(Product product);

        public void UpdateProduct(int productId);
        public void DeleteProduct(int productId);
        public Product GetProductById(int id);
        public List<Product> GetAllProducts();
        public List<Product> SearchProductByName(string name);
        public List<Product> SearchProductByCatagorey(string catagorey);

    }
}
