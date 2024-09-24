using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bt1.Model;

namespace bt1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Projects : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product(1, "Product 1", "Description of Product 1", 10.99m),
            new Product(2, "Product 2", "Description of Product 2", 19.99m),
            new Product(3, "Product 3", "Description of Product 3", 25.99m)
        };

        [HttpGet("")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return products;
        }

        [HttpPost("insert")]
        public IActionResult InsertProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data.");
            }

            if (product.Id == 0)
            {
                product.Id = products.Max(p => p.Id) + 1;
            }

            products.Add(product);
            return Ok(products);
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            return Ok(products);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var productToRemove = products.FirstOrDefault(p => p.Id == id);

            if (productToRemove == null)
            {
                return NotFound("Product not found.");
            }

            products.Remove(productToRemove);
            return Ok(products);
        }
    }
}