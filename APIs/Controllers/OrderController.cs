using Application;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService) 
        {
            _orderService = orderService;
        }
        [HttpPost("AddOrder")]
        public IActionResult AddOrder([FromBody] AddOrderDTO orderDTO)
        {
           var createdOrder= _orderService.AddOrder(orderDTO);
            if(createdOrder==null)
                return BadRequest("Order could not be created.");
            return Ok(new
                {
                message = "Order Created Successfully",
            });
        }

        [HttpDelete("CancelOrder/{id}")]
        public IActionResult DeleteOrder(int id)
        {
             _orderService.CancelOrder(id);
            return Ok();
        }

        [HttpGet ("GetOrderSummary")]
        public ActionResult<GetOrderDTO> getOrder(int userId) 
        {
            return Ok(_orderService.GetOrderSummary(userId));
        }
        [HttpPut("UpdateOrderStatus")]
        [Authorize(Roles ="Admin,Vendor")]
        public IActionResult UpdateOrderStatus(int orderId, string status) 
        {
            if (_orderService.UpdateOrderStatus(orderId, status))
                return Ok(new { message = "Order status updated successfully." });
            else
                return BadRequest(new { message = "Failed to update order status." });
        }
    }
}
