namespace Ecommere_Website.Models
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;


        public  ProductRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public List<Product> SearchProducts(string searchTerm) 
        {

            List<Product> products = new List<Product>();
            products= _context.Products
           .Where(p => p.Name.Contains(searchTerm))
           .ToList();
            return products;
        }
    }
}
