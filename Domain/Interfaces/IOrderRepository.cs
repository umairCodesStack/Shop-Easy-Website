using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        public void AddOrder(AddOrderDTO order);
        public void CancelOrder(int orderId);

        public GetOrderDTO GetOrderSummary(int orderId);


    }
}
