namespace Ecommere_Website.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StoreName {  get; set; }
        public string ImgUrl {  get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country {  get; set; }
        public string City { get; set; }
        public string Address {  get; set; }
        public string PostalCode { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}