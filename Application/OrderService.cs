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
    public class OrderService
    {
        private readonly IOrderRepository _repo;
        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }
        public Order? AddOrder(AddOrderDTO order)
        {
            return _repo.AddOrder(order);
        }
        public void CancelOrder(int orderId)
        {
            _repo.CancelOrder(orderId);
        }
        public List<GetOrderDTO> GetOrderSummaryByUserId(int userId)
        {
            return _repo.GetOrderSummariesByUserId(userId);
        }
        public List<GetOrderDTO> GetOrdersByVendorId(int vendorId)
        {
            return _repo.GetOrdersByVendorId(vendorId);
        }
        public bool UpdateOrderStatus(int orderId, string status)
        {
            return _repo.UpdateOrderStatus(orderId, status);
        }
        public bool RequestOrderCancellation(int orderId, string reason)
        {
            return _repo.RequestOrderCancellation(orderId, reason);
        }
        public List<GetCustomerDTO> GetYourCustomers(int vendorId)
        {
            return _repo.GetYourCustomers(vendorId);
        }
    }
}
