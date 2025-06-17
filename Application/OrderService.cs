using Domain.DTOs;
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
        public void AddOrder(AddOrderDTO order) 
        {
            _repo.AddOrder(order);
        }
        public void CancelOrder(int orderId) 
        {
            _repo.CancelOrder(orderId);
        }
        public GetOrderDTO GetOrderSummary(int orderId) 
        {
            return _repo.GetOrderSummary(orderId);
        }

    }
}
