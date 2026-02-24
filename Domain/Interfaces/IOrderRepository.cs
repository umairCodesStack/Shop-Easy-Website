using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        public Order? AddOrder(AddOrderDTO order);
        public void CancelOrder(int orderId);
        public bool UpdateOrderStatus(int orderId, string status);
        public List<GetOrderDTO> GetOrderSummariesByUserId(int orderId);
        public List<GetOrderDTO> GetOrdersByVendorId(int vendorId);
        public bool RequestOrderCancellation(int orderId, string reason);
        public List<GetCustomerDTO> GetYourCustomers(int vendorId);



    }
}
