using Microsoft.EntityFrameworkCore;
using Product.Infrastructure.Database.EntityFramework.Configurations;

namespace Product.Infrastructure.Database.EntityFramework
{
    /// <summary>
    /// Defines an entity framework context to access the database.
    /// </summary>
    public class ProductContext
        : DbContext
    {
        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.ProductHistory> ProductHistory { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options)
        : base(options)
        { }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductHistoryConfiguration());
        }
    }
}
