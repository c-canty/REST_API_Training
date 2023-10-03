// # nullable disable   to disable nullability for the entire file

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyFirstAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public string Sku { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }
        [JsonIgnore] // This is to prevent a circular reference
        public virtual Category? Category { get; set; }

    }
}
