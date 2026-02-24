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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Order? AddOrder(AddOrderDTO orderDto)
        {

            var newOrder = new Order
            {
                CustomerId = orderDto.customerId,
                CustomerName = orderDto.CustomerName,
                CustomerPhone = orderDto.CustomerPhone,
                CustomerEmail = orderDto.CustomerEmail,
                VendorId = orderDto.vendorId,
                ShippingAddress = orderDto.Address,
                OrderDate = orderDto.OrderDate ?? DateTime.UtcNow,
                Status = "Pending",
                ShippingPrice = orderDto.ShippingCost ?? 0,
                TotalPrice = orderDto.TotalPrice ?? 0,
                TaxPrice = orderDto.TaxPrice,
                PaymentMethod = orderDto.PaymentMethod,
                PaymentStatus = orderDto.PaymentStatus,

                OrderItems = orderDto.orderItems.Select(oi => new OrderItem
                {
                    ProductName = oi.ProductName,
                    ProductColor = oi.ProductColor,
                    ProductSize = oi.ProductSize,
                    ProductFinalPrice = oi.ProductFinalPrice,
                    Quantity = oi.Quantity,
                    ProductImageUrl = oi.ProductImageUrl,
                    ProductId = oi.ProductId

                }).ToList()
            };
            _context.Orders.Add(newOrder);
            int result = _context.SaveChanges();

            return result > 0 ? newOrder : null;
        }
        public void CancelOrder(int orderId)
        {
            Order order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
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

            // Check the incoming status parameter, not the current order status
            if (status.ToLower() == "reject cancellation")
            {
                order.IsCancelled = false;
                order.Status = "Cancellation Rejected";
            }
            else if (status.ToLower() == "cancell")
            {
                order.IsCancelled = true;
                order.Status = "Cancelled";
            }
            else
            {
                // For all other statuses, set as is (with proper casing)
                order.Status = FormatStatusName(status);
            }

            _context.SaveChanges();
            return true;
        }

        // Helper method to format status names consistently
        private string FormatStatusName(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return status;

            // Convert to proper case
            switch (status.ToLower())
            {
                case "pending":
                    return "Pending";
                case "processing":
                    return "Processing";
                case "shipped":
                    return "Shipped";
                case "delivered":
                    return "Delivered";
                case "cancelled":
                    return "Cancelled";
                case "cancellation requested":
                    return "Cancellation Requested";
                case "cancellation rejected":
                    return "Cancellation Rejected";
                default:
                    // Capitalize first letter of each word
                    return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(status.ToLower());
            }
        }
        public bool RequestOrderCancellation(int orderId, string reason)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                return false;
            order.CancellationRequested = true;
            order.CancellationReason = reason;
            order.CancellationRequestedAt = DateTime.UtcNow;
            order.Status = "Cancellation Requested";
            _context.SaveChanges();
            return true;
        }
        public List<GetOrderDTO> GetOrderSummariesByUserId(int userId)
        {
            // First, fetch data from database
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .Include(o => o.Vendor)
                    .ThenInclude(v => v.Store) // Include Store through Vendor
                .Where(o => o.CustomerId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList(); // Execute query and bring data into memory

            // Then, project to DTOs in memory
            var orderDtos = orders.Select(order => new GetOrderDTO
            {
                Id = order.Id,
                VendorId = order.VendorId,
                Address = order.ShippingAddress,
                OrderDate = order.OrderDate,
                Status = order.Status,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                CustomerName = order.Customer.Name,
                StoreName = order.Vendor?.Store?.Name ?? "",
                StoreId = order.Vendor?.Store?.Id ?? 0,
                TotalPrice = order.OrderItems.Sum(oi => oi.ProductFinalPrice * oi.Quantity),
                products = order.OrderItems.Select(item => new OrderItemDTO
                {
                    ProductName = item.ProductName,
                    ProductColor = item.ProductColor,
                    ProductSize = item.ProductSize,
                    ProductFinalPrice = item.ProductFinalPrice,
                    Quantity = item.Quantity,
                    ProductImageUrl = item.ProductImageUrl
                }).ToList()
            }).ToList();

            return orderDtos;
        }
        public List<GetOrderDTO> GetOrdersByVendorId(int vendorId)
        {
            // First, fetch data from database
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .Include(o => o.Vendor)
                    .ThenInclude(v => v.Store) // Include Store through Vendor
                .Where(o => o.VendorId == vendorId)
                .OrderByDescending(o => o.OrderDate)
                .ToList(); // Execute query and bring data into memory

            // Then, project to DTOs in memory
            var orderDtos = orders.Select(order => new GetOrderDTO
            {
                Id = order.Id,
                VendorId = order.VendorId,
                Address = order.ShippingAddress,
                OrderDate = order.OrderDate,
                Status = order.Status,
                ShipppingPrice = order.ShippingPrice,
                CustomerName = order.Customer.Name,
                CustomerPhone = order.CustomerPhone,
                CustomerEmail = order.CustomerEmail,
                CustomerId = order.CustomerId,
                StoreName = order.Vendor?.Store?.Name ?? "",
                StoreId = order.Vendor?.Store?.Id ?? 0,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                IsCancelled = order.IsCancelled,
                CancellationReason = order.CancellationReason,
                CancellationRequestedAt = order.CancellationRequestedAt,
                TotalPrice = order.OrderItems.Sum(oi => oi.ProductFinalPrice * oi.Quantity),
                products = order.OrderItems.Select(item => new OrderItemDTO
                {
                    ProductName = item.ProductName,
                    ProductColor = item.ProductColor,
                    ProductSize = item.ProductSize,
                    ProductFinalPrice = item.ProductFinalPrice,
                    Quantity = item.Quantity,
                    ProductImageUrl = item.ProductImageUrl,
                    ProductId = item.ProductId
                }).ToList()
            }).ToList();

            return orderDtos;
        }
        public List<GetCustomerDTO> GetYourCustomers(int vendorId)
        {
            var customerOrders = _context.Orders
    .Where(o => o.VendorId == vendorId)
    .GroupBy(o => o.CustomerId)
    .Select(g => new GetCustomerDTO
    {
        Id = g.Key,
        Name = g.First().CustomerName,
        Email = g.First().CustomerEmail,
        Phone = g.First().CustomerPhone,
        TotalOrders = g.Count()
    })
    .ToList();
            return customerOrders;
        }
    }
}
