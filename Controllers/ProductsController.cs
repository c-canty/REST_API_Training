using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Models;

namespace MyFirstAPI.Controllers
{
    [Route("api/[controller]")] // A URI for the whole controller, meaning that all the actions will be under this URI with different HTTP verbs
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context; 

        public ProductsController(ShopContext context)
        {
            _context = context;
            _context.Database.EnsureCreated(); // Creates the database if it doesn't exist
        }


        //[HttpGet]
        //public ActionResult<IEnumerable<Product>> GetAllProducts() // ActionResult is used here as it requires no type parameter or input from the user
        //{
        //    var products = _context.Products.ToList(); // ToList() is an extension method that converts the result to a list and converts to JSON
        //    return Ok(products); // Ok() is a method that returns a 200 status code with the result
        //}


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {             
            return Ok(await _context.Products.ToListAsync()); // Ok() is a method that returns a 200 status code with the result. It is automatically converted to JSON
        }

        [HttpGet("{id}")] // A URI for a specific action
        public async Task<IActionResult> GetProduct(int id) // IActionResult is used here as it requires an input from the user
        {
            var product = await _context.Products.FindAsync(id); // Find() is a method that finds an entity with the given primary key values
            if (product == null) // If the product is not found, return a 404 status code with the result
            {
                return NotFound($"Product with id {id} not found"); // NotFound() is a method that returns a 404 status code with the result
            }
            return Ok(product); // Ok() is a method that returns a 200 status code with the result
        }


        [HttpPost]
        public async Task<IActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid) // ModelState.IsValid is a property that returns true if there are no validation errors. Its not neccessary as the [ApiController] attribute does this automatically
            {
                return BadRequest(ModelState); // BadRequest() is a method that returns a 400 status code with the result
            }
            _context.Products.Add(product); // Add() is a method that adds an entity to the context
            await _context.SaveChangesAsync(); // SaveChangesAsync() is a method that saves all changes made in this context to the database
            return CreatedAtAction("GetProduct", new { id = product.Id }, product); // CreatedAtAction() is a method that returns a 201 status code with the result            
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product) // [FromBody] 
        {
            if (id != product.Id) // If the id in the URI doesn't match the id in the body, return a 400 status code with the result
            {
                return BadRequest($"Product with id {id} not found");
            }

            _context.Entry(product).State = EntityState.Modified; // Entry() is a method that returns an object that represents the entity being tracked by the context
            
            try
            {
                await _context.SaveChangesAsync(); 
            }
            catch(DbUpdateConcurrencyException) // DbUpdateConcurrencyException is thrown when an unexpected number of rows are affected during save
            {
                if (_context.Products.Any(p => p.Id == id) ) // If the product doesn't exist, return a 404 status code with the result
                {
                    return NotFound($"Product with id {id} not found");
                }
                else 
                {
                    throw; // If the product exists, throw an exception
                }
            } 
            return NoContent(); // NoContent() is a method that returns a 204 status code with the result
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id); // Find() is a method that finds an entity with the given primary key values
            if (product == null) // If the product is not found, return a 404 status code with the result
            {
                return NotFound($"Product with id {id} not found");
            }
            _context.Products.Remove(product); // Remove() is a method that removes the entity from the context
            await _context.SaveChangesAsync(); // SaveChangesAsync() is a method that saves all changes made in this context to the database
            return NoContent(); // NoContent() is a method that returns a 204 status code with the result            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProducts([FromBody] IEnumerable<int> productIds) // Takes 
        {
            // Check if the provided product IDs are valid and not null or empty
            if (productIds == null || !productIds.Any())
            {
                // Return a 400 status code if the product IDs are null or empty
                return BadRequest("Product IDs cannot be null or empty.");
            }
            // Get all the products with the provided IDs
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

            // Check if the products exist
            if (products == null || !products.Any())
            {   // Return a 404 status code if the products don't exist
                return NotFound("No products found with the provided IDs.");
            }
            // Remove the products from the context
            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();

            return NoContent();
        }




    }
}
