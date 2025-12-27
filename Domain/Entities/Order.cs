using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int CartId { get; set; }
        [ForeignKey ("CartId")]
        public Cart Cart { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int VendorId { get; set; }
        public User Customer { get; set; }
        public User Vendor { get; set; }
        public string? ShippingAddress { get; set; }

        public DateTime? OrderDate { get; set; } = DateTime.UtcNow;
    }
}
