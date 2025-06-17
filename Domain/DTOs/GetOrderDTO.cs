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
        public List<Product> products{ get; set; }
        public decimal ShipppingPrice { get; set; }
        public decimal TotalPrice { get; set; }

       
    }
}
