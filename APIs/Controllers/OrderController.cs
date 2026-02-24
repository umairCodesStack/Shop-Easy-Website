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
        [Authorize(Roles = "Customer")]
        [HttpPost("AddOrder")]
        public IActionResult AddOrder([FromBody] AddOrderDTO orderDTO)
        {
            var createdOrder = _orderService.AddOrder(orderDTO);
            if (createdOrder == null)
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
        [Authorize(Roles = "Customer")]
        [HttpGet("GetOrderSummary")]
        public IActionResult getOrder(int userId)
        {
            var orderSummary = _orderService.GetOrderSummaryByUserId(userId);
            if (orderSummary == null)
                return NotFound($"No order found for user {userId}.");
            return Ok(orderSummary);
        }
        [HttpGet("GetOrders")]
        public IActionResult getOrdersforVendor(int vendorId)
        {
            var orders = _orderService.GetOrdersByVendorId(vendorId);
            return Ok(orders);
        }

        [HttpPut("UpdateOrderStatus")]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult UpdateOrderStatus(int orderId, string status)
        {
            if (_orderService.UpdateOrderStatus(orderId, status))
                return Ok(new { message = "Order status updated successfully." });
            else
                return BadRequest(new { message = "Failed to update order status." });
        }
        [HttpPost("RequestOrderCancellation")]
        [Authorize(Roles = "Customer")]
        public IActionResult RequestOrderCancellation(int orderId, string reason)
        {
            if (_orderService.RequestOrderCancellation(orderId, reason))
                return Ok(new { message = "Order cancellation requested successfully." });
            else
                return BadRequest(new { message = "Failed to request order cancellation." });
        }
        [HttpGet("GetCustomers")]

        public IActionResult GetCustomers(int vendorId)
        {
            var customers = _orderService.GetYourCustomers(vendorId);
            if (customers == null)
            {
                NotFound();
            }
            return Ok(customers);
        }
    }
}
