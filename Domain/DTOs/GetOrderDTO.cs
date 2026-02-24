using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class GetOrderDTO
    {
        public int Id { get; set; }
        public List<OrderItemDTO> products { get; set; }
        public double? ShipppingPrice { get; set; }
        public double TotalPrice { get; set; } = 0;
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public string? Status { get; set; }
        public string? StoreName { get; set; }
        public int? StoreId { get; set; }
        public int? VendorId { get; set; }
        public string? Address { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
        public bool? IsCancelled { get; set; }
        public string? CancellationReason { get; set; }
        public DateTime? CancellationRequestedAt { get; set; }


    }
}
