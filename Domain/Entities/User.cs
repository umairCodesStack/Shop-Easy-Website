using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? imageUrl { get; set; }
        public Cart Carts { get; set; }
        [JsonIgnore]
        public ICollection<Order> OrdersAsCustomer { get; set; } = new HashSet<Order>();
        [JsonIgnore]
        public ICollection<Order> OrdersAsVendor { get; set; } = new HashSet<Order>();
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public Store? Store { get; set; }


    }
}
