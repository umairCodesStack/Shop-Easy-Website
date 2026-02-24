//using Domain.DTOs;
//using Domain.Entities;
//using Domain.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure
//{
//    public class CartRepository:ICartRepository
//    {
//        private readonly ApplicationDbContext _context;
//        public CartRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public Cart AddToCart(AddToCartDTO cartDTO)
//        {
//            if(cartDTO == null || cartDTO.cartItem == null) 
//            {
//                throw new ArgumentNullException("cartDTO or cartItem is null");
//            }
//            Cart cart = _context.Carts
//                .FirstOrDefault(c => c.UserId == cartDTO.userid);
//            if (cart == null) 
//            {
//                cart = new Cart()
//                {
//                    UserId = cartDTO.userid,
//                    Items = new List<CartItem>()
//                    {
//                        new CartItem
//                        {
//                            ProductId = cartDTO.cartItem.productId,
//                            Quantity = cartDTO.cartItem.quantity,
//                            TotalPrice = cartDTO.cartItem.quantity * _context.Products
//                                .Where(p => p.Id == cartDTO.cartItem.productId)
//                                .Select(p => p.Price)
//                                .FirstOrDefault()

//                        }
//                    }
//                };
//                var cart1=_context.Carts.Add(cart);
//                _context.SaveChanges();
//                return cart;

//            }
//            else
//            {
//                var existingItem = cart.Items
//                    .FirstOrDefault(ci => ci.ProductId == cartDTO.cartItem.productId);
//                if (existingItem != null)
//                {
//                    existingItem.Quantity += cartDTO.cartItem.quantity;
//                }
//                else
//                {
//                    cart.Items.Add(new CartItem
//                    {
//                        ProductId = cartDTO.cartItem.productId,
//                        Quantity = cartDTO.cartItem.quantity,
//                        TotalPrice = cartDTO.cartItem.quantity * _context.Products
//                            .Where(p => p.Id == cartDTO.cartItem.productId)
//                            .Select(p => p.Price)
//                            .FirstOrDefault()
//                    });
//                    _context.Carts.Update(cart);
//                    _context.SaveChanges();
//                }
//                return cart;
//            }


//        }
//        public Cart GetCartByUserId(int userId)
//        {
//            return _context.Carts
//                .Include(c => c.Items)
//                .ThenInclude(ci => ci.Product)
//                .FirstOrDefault(c => c.UserId == userId);
//        }
//        public bool RemoveFromCart(int cartItemId)
//        {
//            var cartItem = _context.CartItems.Find(cartItemId);
//            if (cartItem != null)
//            {
//                _context.CartItems.Remove(cartItem);
//                _context.SaveChanges();
//                return true;
//            }
//            return false;

//        }
//        public bool incrementCartItem(int cartItemId) 
//        {
//            var cartItem = _context.CartItems.Find(cartItemId);
//            if (cartItem != null)
//            {
//                cartItem.Quantity++;
//                _context.CartItems.Update(cartItem);
//                _context.SaveChanges();
//                return true;
//            }
//            return false;

//        }

//    }
//}
