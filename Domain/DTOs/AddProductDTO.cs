using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AddProductDTO
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public int? SupplierId { get; set; }
        public List<string> Sizes { get; set; } 
        public List<string> Colors { get; set; } 
        public List<string> ImageUrls { get; set; } 
    }
}
