using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        public int ProductId { get; set; }
        public string? ProductColor { get; set; }
        public string? ProductSize { get; set; }
        public double ProductFinalPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductImageUrl { get; set; }
        [ForeignKey("Order")]

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
