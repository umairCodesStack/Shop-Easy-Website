using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AddOrderDTO
    {
        public int userId {  get; set; }
        public int cartId {  get; set; }
    }
}
