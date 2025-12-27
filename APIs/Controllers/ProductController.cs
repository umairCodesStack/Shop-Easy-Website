using Application;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
             Product createdProduct=_productService.AddProduct(product);

            if(createdProduct==null)
                return BadRequest("Product could not be created.");
            return Ok(new
            {
                message = "Product Added Successfully",
                product = createdProduct,
                productId = createdProduct.Id,
                timestamp = DateTime.UtcNow
            });
        }
        [HttpPut("{product_id}",Name ="updateProduct")]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Put([FromRoute] int product_id, [FromBody] Product product)
        {
            if (product_id != product.Id)
                return BadRequest("Product IDs do not match.");

            _productService.UpdateProduct(product_id);
            return NoContent();
        }

        [HttpDelete("{product_id}", Name = "DeleteProduct")]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Delete([FromRoute] int product_id)
        {
            _productService.DeleteProduct(product_id);
            return NoContent();
        }
    }
}
