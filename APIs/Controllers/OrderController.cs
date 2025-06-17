using Application;
using Domain.DTOs;
using Domain.Entities;
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
            _orderService.AddOrder(orderDTO);
            return Created(); 
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
    }
}
