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
        public IActionResult AddToCart([FromBody] AddToCartDTO value)
        {
            _cartService.AddToCart(value);
            return Ok( new { message = "Item added to cart successfully." });


        }
        [HttpPost("IncrementCartItem")]
        public IActionResult IncrementCartItem([FromQuery] int cartItemId)
        {
            if(_cartService.IncrementCartItem(cartItemId)) 
            {
                return Ok(new { message = "Cart item incremented successfully." });
            }
            else 
            {
                return BadRequest(new { message = "Failed to increment cart item." });
            }
        }
        [HttpPost("RemoveFromCart")]
        public IActionResult RemoveFromCart([FromQuery] int cartItemId)
        {
            if (_cartService.RemoveFromCart(cartItemId))
            {
                return Ok(new { message = "Cart item removed successfully." });
            }
            else
            {
                return BadRequest(new { message = "Failed to remove cart item." });
            }
        }
        [HttpGet("GetCartByUserId/{userId}")]
        public ActionResult<Cart> GetCartByUserId(int userId)
        {
            var cart = _cartService.getCartByUserId(userId);
            if (cart == null)
                return NotFound($"Cart for user {userId} not found.");
            return cart;
        }
        }
}
