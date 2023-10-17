namespace MyFirstAPI.Models
{
    public class ProductQueryPerameters : QueryPerameters
    {
        public decimal? MinPrice { get; set; } // Minimum price of the product
        public decimal? MaxPrice { get; set; } = decimal.MaxValue; // Maximum price of the product
    }
}
