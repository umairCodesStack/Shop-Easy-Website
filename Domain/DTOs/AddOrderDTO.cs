using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;

namespace Domain.DTOs
{
    public class AddOrderDTO
    {
        public int customerId { get; set; }
        public int vendorId { get; set; }
        public List<OrderItemDTO> orderItems { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Address { get; set; }
        public string? PaymentMethod { get; set; }
        public double? ShippingCost { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public string PaymentStatus { get; set; } = "Pending";
        public double? TotalPrice { get; set; }
        public double TaxPrice { get; set; } = 0;
    }
}
