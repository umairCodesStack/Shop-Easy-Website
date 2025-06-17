using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public void AddOrder(AddOrderDTO order) 
        {
            Order existingOrder = _context.Orders.FirstOrDefault(o=>o.userId==order.userId);
            if (existingOrder != null) 
            {
                Order newOrder = new Order
                {
                    userId = order.userId,
                    CartId = order.cartId,
                };
                _context.Orders.Add(newOrder);  
                _context.SaveChanges();
            }
        }
        public void CancelOrder(int orderId) 
        {
            Order order = _context.Orders.FirstOrDefault(o=>o.userId==orderId);
            if (order != null) 
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
        public GetOrderDTO GetOrderSummary(int userId)
        {
            var order = _context.Orders
                .Include(o => o.Cart)
                    .ThenInclude(c => c.Items)
                        .ThenInclude(ci => ci.Product)
                .FirstOrDefault(o => o.userId == userId);

            if (order == null)
                return null;

            var productList = order.Cart.Items.Select(item => new Product
            {
                Id = item.Product.Id,
                Name = item.Product.Name,
                StockQuantity = item.Quantity,
                Price = item.Quantity * item.Product.Price 
            }).ToList();

            var orderSummary = new GetOrderDTO
            {
                Id = order.Id,
                products = productList
            };

            return orderSummary;
        }


    }
}
