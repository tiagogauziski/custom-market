using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Product.Infrastructure.Database.MongoDB.Settings;
using Product.Models;

namespace Product.Infrastructure.Database.MongoDB.Repositories
{
    /// <summary>
    /// MongoDB implementation of <see cref="IProductHistoryRepository"/>.
    /// </summary>
    public class ProductHistoryRepository :
        IProductHistoryRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<ProductHistory> _productHistoryCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductHistoryRepository"/> class.
        /// </summary>
        /// <param name="mongoClient">MongoDB database client.</param>
        /// <param name="productDatabaseSettings">Product database settings.</param>
        public ProductHistoryRepository(
            MongoClient mongoClient,
            IProductDatabaseSettings productDatabaseSettings)
        {
            _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));

            IMongoDatabase database = _mongoClient.GetDatabase(productDatabaseSettings.DatabaseName);

            _productHistoryCollection = database.GetCollection<ProductHistory>("product-history");
        }

        /// <inheritdoc />
        public async Task SaveEventAsync(EventBase systemEvent, CancellationToken cancellationToken)
        {
            ProductHistory productHistory = new ProductHistory
            {
                Id = Guid.NewGuid(),
                ProductId = systemEvent.ObjectId,
                Changes = systemEvent.GetChanges(),
                DateTimeUtc = DateTimeOffset.UtcNow,
                EventName = systemEvent.EventName,
            };

            await _productHistoryCollection.InsertOneAsync(productHistory, null, cancellationToken).ConfigureAwait(false);
        }
    }
}
