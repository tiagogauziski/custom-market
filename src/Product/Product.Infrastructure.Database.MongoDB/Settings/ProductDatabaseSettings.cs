using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace Product.Infrastructure.Database.MongoDB.Settings
{
    /// <summary>
    /// Default implementation of <see cref="IProductDatabaseSettings"/>.
    /// </summary>
    public class ProductDatabaseSettings : IOptions<ProductDatabaseSettings>, IProductDatabaseSettings
    {
        /// <inheritdoc />
        [Required]
        public string ConnectionString { get; set; }

        /// <inheritdoc />
        [Required]
        public string DatabaseName { get; set; }

        /// <inheritdoc />
        ProductDatabaseSettings IOptions<ProductDatabaseSettings>.Value => this;
    }
}
