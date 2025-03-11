namespace Ecommere_Website.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } // Fixed the typo
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<ProductSize> Sizes { get; set; } = new HashSet<ProductSize>();
        public ICollection<ProductColor> Colors { get; set; } = new HashSet<ProductColor>();
        public ICollection<ProductImage> ImageUrls { get; set; } = new HashSet<ProductImage>();
    }
}
