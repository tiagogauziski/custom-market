using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Models;

namespace Product.Infrastructure.Database.EntityFramework.Configurations
{
    public class ProductHistoryConfiguration
        : IEntityTypeConfiguration<ProductHistory>
    {
        public void Configure(EntityTypeBuilder<ProductHistory> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.EventName)
                .IsRequired(true);
        }
    }
}
