namespace Ecommere_Website.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } 
        public int StockQuantity { get; set; }
        public int quantity {  get; set; }
        public decimal UnitPrice {  get; set; }
        public decimal TotalPrice { get; set; }
        public int Rating { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public List<ProductSize> Sizes { get; set; }
        public List<ProductColor> Colors { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }

}
