using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AddToCartDTO
    {
        public int userid {  get; set; }
        public AddCartItemDTO cartItem {  get; set; }
    }
}
