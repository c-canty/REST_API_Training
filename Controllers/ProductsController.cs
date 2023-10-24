using HPlusSport.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Models;
using Asp.Versioning;

namespace MyFirstAPI.Controllers
{
    [ApiVersion("1.0")] // Sets the version of the API

    [Route("products")] // A URI for the whole controller, meaning that all the actions will be under this URI with different HTTP verbs
    [ApiController]
    public class ProductsV1Controller : ControllerBase 
    {
        private readonly ShopContext _context; 

        public ProductsV1Controller(ShopContext context)
        {
            _context = context;
            _context.Database.EnsureCreated(); // Creates the database if it doesn't exist
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductQueryPerameters queryPerameters)
        {   
            IQueryable<Product> products = _context.Products; // IQueryable is an interface that allows querying of a specific type of data

            if (queryPerameters.MinPrice != null) // If the minimum price is not the default value of decimal, return the products with a price greater than or equal to the minimum price
            {
                products = products.Where(p => p.Price >= queryPerameters.MinPrice.Value); // Where() is a method that filters a sequence of values based on a predicate
            }
            if (queryPerameters.MaxPrice != null) // If the maximum price is not the default value of decimal, return the products with a price less than or equal to the maximum price
            {
                products = products.Where(p => p.Price <= queryPerameters.MaxPrice.Value); // Where() is a method that filters a sequence of values based on a predicate
            }
            if (!string.IsNullOrEmpty(queryPerameters.Name)) // If the name is not null or empty, return the products with a name that contains the name
            {
                products = products.Where(p => p.Name.ToLower().Contains(queryPerameters.Name.ToLower()) || p.Sku.ToLower().Contains(queryPerameters.Name.ToLower())); // Where() is a method that filters a sequence of values based on a predicate
            }
            if (!string.IsNullOrEmpty(queryPerameters.Sku)) // If the SKU is not null or empty, return the products with a SKU that contains the SKU
            {
                products = products.Where(p => p.Sku.Equals(queryPerameters.Sku)); // Where() is a method that filters a sequence of values based on a predicate
            }
            if (!string.IsNullOrEmpty(queryPerameters.SortBy))
            {
                if(typeof(Product).GetProperty(queryPerameters.SortBy) != null)
                {
                    products = products.OrderByCustom(
                        queryPerameters.SortBy,
                        queryPerameters.SortOrder);
                }
            }

            products = products
                .Skip(queryPerameters.Size * (queryPerameters.Page -1)) // Skip() is a method that skips a specified number of elements in a sequence and returns the remaining elements
                .Take(queryPerameters.Size); // Take() is a method that returns a specified number of contiguous elements from the start of a sequence

            return Ok(await products.ToListAsync()); // Ok() is a method that returns a 200 status code with the result. It is automatically converted to JSON
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

    //[ApiVersion("2.0")] // Sets the version of the API
    //[Route("v{v:apiVersion}/products")] // A URI for the whole controller, meaning that all the actions will be under this URI with different HTTP verbs
    ////[Route("products")] // A URI for the whole controller, meaning that all the actions will be under this URI with different HTTP verbs
    //[ApiController]
    //public class ProductsV2Controller : ControllerBase
    //{
    //    private readonly ShopContext _context;

    //    public ProductsV2Controller(ShopContext context)
    //    {
    //        _context = context;
    //        _context.Database.EnsureCreated(); // Creates the database if it doesn't exist
    //    }


    //    [HttpGet]
    //    public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryPerameters queryPerameters)
    //    {
    //        IQueryable<Product> products = _context.Products; // IQueryable is an interface that allows querying of a specific type of data

    //        products = products.Where(p => p.IsAvailable == true); // Where() is a method that filters a sequence of values based on a predicate

    //        products = products
    //            .Skip(queryPerameters.Size * (queryPerameters.Page - 1)) // Skip() is a method that skips a specified number of elements in a sequence and returns the remaining elements
    //            .Take(queryPerameters.Size); // Take() is a method that returns a specified number of contiguous elements from the start of a sequence

    //        return Ok(await products.ToListAsync()); // Ok() is a method that returns a 200 status code with the result. It is automatically converted to JSON
    //    }

    //    [HttpGet("{id}")] // A URI for a specific action
    //    public async Task<IActionResult> GetProduct(int id) // IActionResult is used here as it requires an input from the user
    //    {
    //        var product = await _context.Products.FindAsync(id); // Find() is a method that finds an entity with the given primary key values
    //        if (product == null) // If the product is not found, return a 404 status code with the result
    //        {
    //            return NotFound($"Product with id {id} not found"); // NotFound() is a method that returns a 404 status code with the result
    //        }
    //        return Ok(product); // Ok() is a method that returns a 200 status code with the result
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> PostProduct(Product product)
    //    {
    //        if (!ModelState.IsValid) // ModelState.IsValid is a property that returns true if there are no validation errors. Its not neccessary as the [ApiController] attribute does this automatically
    //        {
    //            return BadRequest(ModelState); // BadRequest() is a method that returns a 400 status code with the result
    //        }
    //        _context.Products.Add(product); // Add() is a method that adds an entity to the context
    //        await _context.SaveChangesAsync(); // SaveChangesAsync() is a method that saves all changes made in this context to the database
    //        return CreatedAtAction("GetProduct", new { id = product.Id }, product); // CreatedAtAction() is a method that returns a 201 status code with the result            
    //    }


    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutProduct(int id, Product product) // [FromBody] 
    //    {
    //        if (id != product.Id) // If the id in the URI doesn't match the id in the body, return a 400 status code with the result
    //        {
    //            return BadRequest($"Product with id {id} not found");
    //        }

    //        _context.Entry(product).State = EntityState.Modified; // Entry() is a method that returns an object that represents the entity being tracked by the context

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException) // DbUpdateConcurrencyException is thrown when an unexpected number of rows are affected during save
    //        {
    //            if (_context.Products.Any(p => p.Id == id)) // If the product doesn't exist, return a 404 status code with the result
    //            {
    //                return NotFound($"Product with id {id} not found");
    //            }
    //            else
    //            {
    //                throw; // If the product exists, throw an exception
    //            }
    //        }
    //        return NoContent(); // NoContent() is a method that returns a 204 status code with the result
    //    }


    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteProduct(int id)
    //    {
    //        var product = await _context.Products.FindAsync(id); // Find() is a method that finds an entity with the given primary key values
    //        if (product == null) // If the product is not found, return a 404 status code with the result
    //        {
    //            return NotFound($"Product with id {id} not found");
    //        }
    //        _context.Products.Remove(product); // Remove() is a method that removes the entity from the context
    //        await _context.SaveChangesAsync(); // SaveChangesAsync() is a method that saves all changes made in this context to the database
    //        return NoContent(); // NoContent() is a method that returns a 204 status code with the result            
    //    }

    //    [HttpDelete]
    //    public async Task<IActionResult> DeleteProducts([FromBody] IEnumerable<int> productIds) // Takes 
    //    {
    //        // Check if the provided product IDs are valid and not null or empty
    //        if (productIds == null || !productIds.Any())
    //        {
    //            // Return a 400 status code if the product IDs are null or empty
    //            return BadRequest("Product IDs cannot be null or empty.");
    //        }
    //        // Get all the products with the provided IDs
    //        var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

    //        // Check if the products exist
    //        if (products == null || !products.Any())
    //        {   // Return a 404 status code if the products don't exist
    //            return NotFound("No products found with the provided IDs.");
    //        }
    //        // Remove the products from the context
    //        _context.Products.RemoveRange(products);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //}
}
