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
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        // FK
        public int StoreId { get; set; }

        public string Category { get; set; }

        public List<string>? Sizes { get; set; }

        public List<string>? Colors { get; set; }

        public List<string>? ImageUrls { get; set; }
        public double? discount { get; set; }
        public string? tag { get; set; }
        public int userId { get; set; }
    }
}
