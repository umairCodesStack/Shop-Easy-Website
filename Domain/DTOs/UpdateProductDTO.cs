using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UpdateProductDTO
    {

        public string? Name { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public decimal? Price { get; set; } = null;

        public int? StockQuantity { get; set; } = null;

        public string? Category { get; set; } = null;

        public List<string>? SizesToRemove { get; set; }

        public List<string>? ColorsToRemove { get; set; }
        public List<string>? NewSizes { get; set; }

        public List<string>? NewColors { get; set; }

        public List<string>? ImageUrlsToRemove { get; set; }
        public List<string>? NewImageUrls { get; set; }
        public double? discount { get; set; }
        public string? tag { get; set; }
    }
}
