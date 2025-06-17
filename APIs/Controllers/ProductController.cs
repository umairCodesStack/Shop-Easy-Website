using Application;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{product_id}")]
        public IActionResult Get([FromRoute] int product_id)
        {
            var product = _productService.GetProductById(product_id);
            if (product == null)
                return NotFound($"Product with id {product_id} not found.");

            return Ok(product);
        }


        [HttpPost("AddProduct")]
        public IActionResult Post([FromBody] AddProductDTO product)
        {
             _productService.AddProduct(product);

            // Assuming createdProduct contains the newly created product with an Id
            return Ok();
        }



        [HttpPut("{product_id}")]
        public IActionResult Put([FromRoute] int product_id, [FromBody] Product product)
        {
            if (product_id != product.Id)
                return BadRequest("Product IDs do not match.");

            _productService.UpdateProduct(product_id);
            return NoContent();
        }

        [HttpDelete("{product_id}")]
        public IActionResult Delete([FromRoute] int product_id)
        {
            _productService.DeleteProduct(product_id);
            return NoContent();
        }
    }
}
