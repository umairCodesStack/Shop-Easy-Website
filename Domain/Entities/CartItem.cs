using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int cartId { get; set; }
        [ForeignKey("cartId")]
       
        [JsonIgnore]
        public Cart cart { get; set; }
        public int ProductId { get; set; }
        [ForeignKey ("ProductId")]
        public Product Product { get; set; }
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }
    }
}
