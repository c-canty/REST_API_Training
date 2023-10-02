using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstAPI.Controllers
{
    [Route("api/[controller]")] // A URI for the whole controller, meaning that all the actions will be under this URI with different HTTP verbs
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetProducts()
        {
            return "All products";
        }

        //[HttpGet]
        //public IActionResult GetProducts()
        //{
        //    return Ok("All products");
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetProduct(int id)
        //{
        //    return Ok($"Product with id {id}");
        //}

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
