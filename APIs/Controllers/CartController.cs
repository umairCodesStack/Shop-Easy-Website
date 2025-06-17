using Application;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        
        private readonly CartService _cartService;
        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost ("AddToCart")]
        public void AddToCart([FromBody] AddToCartDTO value)
        {
            _cartService.AddToCart(value);

                
        }
        [HttpPost("IncrementCartItem")]
        public void IncrementCartItem([FromBody] int cartItemId)
        {
            _cartService.IncrementCartItem(cartItemId);
        }
        [HttpPost("RemoveFromCart")]
        public void RemoveFromCart([FromBody] int cartItemId)
        {
            _cartService.RemoveFromCart(cartItemId);
        }
        [HttpGet("GetCartByUserId/{userId}")]
        public ActionResult<Cart> GetCartByUserId(string userId)
        {
            var cart = _cartService.getCartByUserId(userId);
            if (cart == null)
                return NotFound($"Cart for user {userId} not found.");
            return cart;
        }
        }
}
