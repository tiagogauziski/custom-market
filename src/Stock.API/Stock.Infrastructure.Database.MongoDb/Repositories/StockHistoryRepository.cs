using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Stock.Domain.Models;
using Stock.Infrastructure.Database.Abstractions;
using Stock.Infrastructure.Database.MongoDb.Settings;

namespace Stock.Infrastructure.Database.MongoDb.Repositories
{
    /// <summary>
    /// MongoDB implementation of <see cref="IStockHistoryRepository"/>.
    /// </summary>
    public class StockHistoryRepository : IStockHistoryRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<StockHistoryModel> _stockHistoryCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockHistoryRepository"/> class.
        /// </summary>
        /// <param name="mongoClient">MongoDB database client.</param>
        /// <param name="productDatabaseSettings">Product database settings.</param>
        public StockHistoryRepository(
            MongoClient mongoClient,
            IStockDatabaseSettings productDatabaseSettings)
        {
            _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));

            IMongoDatabase database = _mongoClient.GetDatabase(productDatabaseSettings.DatabaseName);

            _stockHistoryCollection = database.GetCollection<StockHistoryModel>("stock-history");
        }

        /// <inheritdoc />
        public async Task DecreaseStockEventAsync(StockHistoryModel decreaseStockModel, CancellationToken cancellationToken)
        {
            await _stockHistoryCollection.InsertOneAsync(decreaseStockModel, null, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task IncreaseStockEventAsync(StockHistoryModel increaseStockModel, CancellationToken cancellationToken)
        {
            await _stockHistoryCollection.InsertOneAsync(increaseStockModel, null, cancellationToken).ConfigureAwait(false);
        }
    }
}
