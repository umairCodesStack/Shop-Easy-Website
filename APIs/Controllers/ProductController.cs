using Application;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq;

namespace APIs.Controllers
{
    [Route("odata/[controller]")]
    public class ProductController : ODataController
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }


        [EnableQuery]
        [HttpGet]
        public IQueryable<GetProductDTO> Get()
        {
            return _productService.GetAllProducts().AsQueryable();
        }


        [EnableQuery]
        [HttpGet("getProductById")]
        public IActionResult Get([FromQuery] int product_id)
        {
            var product = _productService.GetProductById(product_id);
            if (product == null)
                return NotFound($"Product with id {product_id} not found.");

            return Ok(product);
        }
        [HttpPost("AddProduct")]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Post([FromBody] AddProductDTO product)
        {
            Product createdProduct = _productService.AddProduct(product);

            if (createdProduct == null)
                return BadRequest("Product could not be created.");
            return Ok(new
            {
                message = "Product Added Successfully",
                product = createdProduct,
                productId = createdProduct.Id,
                timestamp = DateTime.UtcNow
            });
        }

        [Authorize(Roles = "Admin,Vendor")]
        [HttpPut("{productId}")]

        public IActionResult UpdateProduct(int productId, [FromBody] UpdateProductDTO productDTO)
        {
            try
            {
                // Validate model
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid product data", errors = ModelState });
                }

                // Update product
                bool success = _productService.UpdateProduct(productId, productDTO);

                if (success)
                {
                    return Ok(new { message = "Product updated successfully", productId = productId });
                }
                else
                {
                    return NotFound(new { message = $"Product with id {productId} not found" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating product {productId}: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while updating the product", error = ex.Message });
            }
        }

        [HttpDelete("DeleteProduct")]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Delete(int product_id)
        {
            int res = _productService.DeleteProduct(product_id);
            if (res == 0)
                return NotFound($"Product with id {product_id} not found.");
            return Ok("Product Deleted Successfuly");
        }
        [HttpGet("getCatagories")]
        public IActionResult GetCatagories()
        {
            var catagories = _productService.GetCatagories();
            return Ok(catagories);
        }
        [HttpGet("getProductByUserId")]
        public IActionResult GetProductByUserId(int userId)
        {
            var products = _productService.getProductByUserId(userId);
            if (products == null || products.Count == 0)
                return NotFound($"No products found for user with id {userId}.");
            return Ok(products);
        }
    }
}
