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


        //[HttpPost]
        //public IActionResult CreateProduct()
        //{
        //    return Ok("Product created");
        //}


        //[HttpPut("{id}")]
        //public IActionResult UpdateProduct(int id)
        //{
        //    return Ok($"Product with id {id} updated");
        //}


        //[HttpDelete("{id}")]
        //public IActionResult DeleteProduct(int id)
        //{
        //    return Ok($"Product with id {id} deleted");
        //}
    }
}
