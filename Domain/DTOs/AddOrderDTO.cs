using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AddOrderDTO
    {
        public int customerId {  get; set; }
        public int vendorId {  get; set; }
        public int cartId {  get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Address { get; set; }
    }
}
