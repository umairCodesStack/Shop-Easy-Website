using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class OrderItemDTO
    {
        public string ProductName { get; set; }
        public string? ProductColor { get; set; }
        public string? ProductSize { get; set; }
        public double ProductFinalPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductImageUrl { get; set; }
        public int ProductId { get; set; }

    }
}
