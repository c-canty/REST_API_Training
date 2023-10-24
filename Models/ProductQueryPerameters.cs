namespace MyFirstAPI.Models
{
    public class ProductQueryPerameters : QueryPerameters
    {
        public decimal? MinPrice { get; set; } // Minimum price of the product
        public decimal? MaxPrice { get; set; } = decimal.MaxValue; // Maximum price of the product

        public string? Name { get; set; } = string.Empty; // Name of the product
        public string? Sku { get; set; } = string.Empty; // Stock keeping unit of the product
    }
}
