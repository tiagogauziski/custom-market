using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace Stock.Infrastructure.Database.MongoDb.Settings
{
    /// <summary>
    /// Default implementation of <see cref="IStockDatabaseSettings"/>.
    /// Store stock specific database settings for MongoDB.
    /// </summary>
    public class StockDatabaseSettings : IOptions<StockDatabaseSettings>, IStockDatabaseSettings
    {
        /// <inheritdoc />
        [Required]
        public string ConnectionString { get; set; }

        /// <inheritdoc />
        [Required]
        public string DatabaseName { get; set; }

        /// <inheritdoc />
        StockDatabaseSettings IOptions<StockDatabaseSettings>.Value => this;
    }
}
