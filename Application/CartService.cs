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
        public void AddToCart(AddToCartDTO cart)
        {
             _cartRepository.AddToCart(cart);
        }
        public void IncrementCartItem(int cartItemId)
        {
            _cartRepository.incrementCartItem(cartItemId);
        }
        public void RemoveFromCart(int cartItemId)
        {
            _cartRepository.RemoveFromCart(cartItemId);
        }
        public Cart getCartByUserId(string userId)
        {
            return _cartRepository.GetCartByUserId(userId);
        }
    }
}
