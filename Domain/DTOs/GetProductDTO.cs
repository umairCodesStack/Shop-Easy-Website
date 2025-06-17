using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public int StockQuantity { get; set; }

        public decimal Price { get; set; }

        public int Rating { get; set; }

        public int? SupplierId { get; set; }

        public List<string> Sizes { get; set; } = new();

        public List<string> Colors{ get; set; } = new();

        public List<string> ImageUrls{ get; set; } = new();
    }
}
