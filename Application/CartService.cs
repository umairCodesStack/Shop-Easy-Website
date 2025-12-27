using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public Cart AddToCart(AddToCartDTO cart)
        {
             return _cartRepository.AddToCart(cart);
        }
        public bool IncrementCartItem(int cartItemId)
        {
           return _cartRepository.incrementCartItem(cartItemId);
        }
        public bool RemoveFromCart(int cartItemId)
        {
            return _cartRepository.RemoveFromCart(cartItemId);
        }
        public Cart getCartByUserId(int userId)
        {
            return _cartRepository.GetCartByUserId(userId);
        }
    }
}
