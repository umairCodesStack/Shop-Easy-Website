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
        public Order AddOrder(AddOrderDTO order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Order data cannot be null");
            Order newOrder = new Order
            {
                CustomerId = order.customerId,
                VendorId = order.vendorId,
                CartId = order.cartId,
                CustomerName = _context.Users.Find(order.customerId)?.Name,
                OrderDate = DateTime.UtcNow,
                ShippingAddress = order.Address,
                Status = "Pending",
            };
            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            return newOrder; 
        }
        public void CancelOrder(int orderId) 
        {
            Order order = _context.Orders.FirstOrDefault(o=>o.Id==orderId);
            if (order != null) 
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
        public bool UpdateOrderStatus(int orderId, string status)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                return false;
            order.Status = status;
            _context.SaveChanges();
            return true;
        }
        public GetOrderDTO GetOrderSummary(int userId)
        {
            var order = _context.Orders
                .Include(o => o.Cart)
                    .ThenInclude(c => c.Items)
                        .ThenInclude(ci => ci.Product)
                .FirstOrDefault(o => o.CustomerId == userId);

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
                products = productList,
                Status= order.Status,
            };

            return orderSummary;
        }
        public List<GetOrderDTO> GetOrdersByVendorId(int vendorId)
        {
            // Get all orders for the vendor
            List<Order> orders = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Vendor)
                .Include(o => o.Cart)
                    .ThenInclude(c => c.Items)
                .Where(o => o.VendorId == vendorId) // Use Where, not ToList
                .ToList();

            if (orders == null || !orders.Any())
                return new List<GetOrderDTO>();

            // Map each order to GetOrderDTO
            var orderDTOs = orders.Select(order => new GetOrderDTO
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CustomerName = order.Customer?.Name,
                VendorId = order.VendorId,
                VendorName = order.Vendor?.Name,
                OrderDate = order.OrderDate,
                Status = order.Status,
                products=order.Cart.Items.Select(item => new Product
                {
                    Id = item.Product.Id,
                    Name = item.Product.Name,
                    StockQuantity = item.Quantity,
                    Price = item.Quantity * item.Product.Price
                }).ToList()
            }).ToList();

            return orderDTOs;
        }
    }
}
