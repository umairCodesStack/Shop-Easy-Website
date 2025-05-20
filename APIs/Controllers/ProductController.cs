using Application;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
		private readonly ProductService _product_service;

		public ProductController(ProductService product_service)
		{
			_product_service = product_service;
		}

		// GET api/products
		[HttpGet]
		public ActionResult<List<Product>> get_all_products()
		{
			return _product_service.GetAllProducts();
		}

		// GET api/products/5
		[HttpGet("{product_id:int}")]
		public ActionResult<Product> get_product_by_id(int product_id)
		{
			var product = _product_service.GetProductById(product_id);
			if (product == null)
				return NotFound($"Product with id {product_id} not found.");

			return product;
		}

		// GET api/products/search?name=aspirin
		[HttpGet("search")]
		public ActionResult<List<Product>> search_by_name([FromQuery] string name)
		{
			return _product_service.SearchProductByName(name);
		}

		// GET api/products/category/paracetamol
		[HttpGet("category/{category}")]
		public ActionResult<List<Product>> get_by_category(string category)
		{
			return _product_service.SearchProductByCatagorey(category);
		}

		// POST api/products
		[HttpPost]
		public IActionResult add_product([FromBody] Product product)
		{
			_product_service.AddProduct(product);
			// Assumes Product has an Id property that’s set during Add
			return CreatedAtAction(nameof(get_product_by_id),
								   new { product_id = product.Id },
								   product);
		}

		// PUT api/products/5
		[HttpPut("{product_id:int}")]
		public IActionResult update_product(int product_id, [FromBody] Product product)
		{
			if (product_id != product.Id)
				return BadRequest("Product ids do not match.");

			_product_service.UpdateProduct(product_id);
			return NoContent();
		}

		// DELETE api/products/5
		[HttpDelete("{product_id:int}")]
		public IActionResult delete_product(int product_id)
		{
			_product_service.DeleteProduct(product_id);
			return NoContent();
		}
	}
}
