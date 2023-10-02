using HPlusSport.API.Models;
using Microsoft.EntityFrameworkCore;


namespace MyFirstAPI.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder) // This is to configure the relationship between the two entities
        {
            // This is to prevent a circular reference
            modelBuilder.Entity<Product>() 
                .HasOne(p => p.Category) // A product has one catagory
                .WithMany(c => c.Products) // A catagory has many products
                .HasForeignKey(p => p.CategoryId); // The foreign key is CatagoryId

            modelBuilder.Seed(); // This is to seed the database, which comes from the ModelBuilderExtensions class which extends the ModelBuilder class functionality
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Catagories { get; set; } = null!;
    }
}
