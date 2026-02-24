using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class GetProductDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public double Rating { get; set; }
        public string StoreName { get; set; }
        public double? Discount { get; set; }
        public string? StoreLogoUrl { get; set; }
        public int? ReviewsCount { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<string> Sizes { get; set; }
        public List<string> Colors { get; set; }
        public string? Tag { get; set; }
        public int StockQuantity { get; set; }
        public int StoreId { get; set; }
        public int VendorId { get; set; }
    }
}
