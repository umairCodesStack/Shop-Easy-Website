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
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public int VendorId { get; set; }
        public User Customer { get; set; }
        public User Vendor { get; set; }
        public string? ShippingAddress { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
        public double TotalPrice { get; set; }
        public double ShippingPrice { get; set; }
        public double TaxPrice { get; set; }
        public bool IsCancelled { get; set; } = false;
        public bool CancellationRequested { get; set; } = false;
        public string? CancellationReason { get; set; }
        public DateTime? CancellationRequestedAt { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public DateTime? OrderDate { get; set; } = DateTime.UtcNow;
    }
}
