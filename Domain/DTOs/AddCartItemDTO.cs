using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AddCartItemDTO
    {
        public int productId { get; set; }
        public int quantity { get; set; }

    }
}
