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
        public void AddToCart(AddToCartDTO cart);
        public Cart GetCartByUserId(string userId);
        public void incrementCartItem(int cartItemId);
        public void RemoveFromCart(int cartItemId);
        
    }

}
