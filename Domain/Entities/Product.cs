using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public int userId { get; set; }
        public double? discount { get; set; }

        public double Rating { get; set; } = 0;
        public int StoreId { get; set; }
        public Store Store { get; set; } = null!;


        // Navigation property
        public ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public string? tag { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public ICollection<ProductSize>? Sizes { get; set; } = new HashSet<ProductSize>();
        [JsonIgnore]
        public ICollection<ProductColor>? Colors { get; set; } = new HashSet<ProductColor>();
        [JsonIgnore]
        public ICollection<ProductImage>? ImageUrls { get; set; } = new HashSet<ProductImage>();
    }
}
