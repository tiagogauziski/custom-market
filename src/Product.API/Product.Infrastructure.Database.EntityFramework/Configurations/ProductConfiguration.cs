using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Infrastructure.Database.EntityFramework.Configurations
{
    public class ProductConfiguration
        : IEntityTypeConfiguration<Models.Product>
    {
        public void Configure(EntityTypeBuilder<Models.Product> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.Name)
                .IsRequired(true);

            builder
                .Property(p => p.Brand)
                .IsRequired(true);
        }
    }
}
