

using System.Text.Json.Serialization;

namespace MyFirstAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string Sku { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        [JsonIgnore] // This is to prevent a circular reference
        public virtual Category Category { get; set; }

    }
}
