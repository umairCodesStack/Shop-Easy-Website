using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICartRepository
    {
        public Cart AddToCart(AddToCartDTO cart);
        public Cart GetCartByUserId(int userId);
        public bool incrementCartItem(int cartItemId);
        public bool RemoveFromCart(int cartItemId);
        
    }

}
