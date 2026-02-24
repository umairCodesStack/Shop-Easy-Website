using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase

    {
        private readonly StoreService _storeService;

        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }
        [Authorize(Roles = "Vendor")]
        [HttpPost("addStore")]
        public IActionResult AddStore(AddStoreDTO store)
        {
            var result = _storeService.AddStore(store);
            if (result == null)
            {
                return BadRequest("Failed to add store");
            }
            return Ok("Store added successfully");
        }

        [HttpGet("getStoreByOwnerId")]
        public IActionResult GetStoreByOwnerId(int ownerId)
        {
            var result = _storeService.GetStoreByOwnerId(ownerId);
            if (result == null)
            {
                return NotFound("Store not found");
            }
            return Ok(result);

        }
        [HttpGet("getAllStores")]
        public IActionResult GetAllStores()
        {
            var result = _storeService.GetAllStores();
            if (result == null)
            {
                return NotFound("No stores found");
            }
            return Ok(result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("updateStoreApprovalStatus")]
        public IActionResult UpdateStoreApprovalStatus(int id, string approvalStatus)
        {
            var result = _storeService.UpdateStoreApprovalStatus(id, approvalStatus);
            if (result == 0)
            {
                return BadRequest("Failed to update store approval status");
            }
            return Ok("Store approval status updated successfully");



        }
        [Authorize(Roles = "Vendor,Admin")]
        [HttpDelete("deleteStore")]
        public IActionResult DeleteStore(int id)
        {
            var result = _storeService.DeleteStore(id);
            if (result == 0)
            {
                return BadRequest("Failed to delete store");
            }
            return Ok("Store deleted successfully");
        }
        [Authorize(Roles = "Vendor")]
        [HttpPut("updateStore")]
        public IActionResult UpdateStore(UpdateStoreDTO store)
        {
            var result = _storeService.UpdateStore(store);
            if (result == 0)
            {
                return BadRequest("Failed to update store");
            }
            return Ok("Store updated successfully");
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("updateStoreStatus")]
        public IActionResult UpdateStoreStatus(int id, string newStatus)
        {
            var result = _storeService.UpdateStoreStatus(id, newStatus);
            if (!result)
            {
                return BadRequest("Failed to update store status");
            }
            return Ok("Store status updated successfully");
        }
    }
}
