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
    /// MongoDB implementastion of <see cref="IStockRepository"/>.
    /// </summary>
    public class StockRepository : IStockRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<StockModel> _stockCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockRepository"/> class.
        /// </summary>
        /// <param name="mongoClient">MongoDB database client.</param>
        /// <param name="productDatabaseSettings">Product database settings.</param>
        public StockRepository(
            MongoClient mongoClient,
            IStockDatabaseSettings productDatabaseSettings)
        {
            _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));

            IMongoDatabase database = _mongoClient.GetDatabase(productDatabaseSettings.DatabaseName);

            _stockCollection = database.GetCollection<StockModel>("stock");
        }

        /// <inheritdoc />
        public async Task DecreaseStockAsync(DecreaseStockModel decreaseStockModel, CancellationToken cancellationToken = default)
        {
            if (decreaseStockModel is null)
            {
                throw new ArgumentNullException(nameof(decreaseStockModel));
            }

            StockModel stock = await GetStockAsync(decreaseStockModel.ProductId, cancellationToken).ConfigureAwait(false);
            if (stock is null)
            {
                stock = new StockModel(decreaseStockModel.ProductId, decreaseStockModel.Quantity);
            }
            else
            {
                stock.Quantity -= decreaseStockModel.Quantity;
            }

            await _stockCollection.ReplaceOneAsync(
                (dbStock) => dbStock.ProductId == decreaseStockModel.ProductId,
                stock,
                new ReplaceOptions() { IsUpsert = true },
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<StockModel> GetStockAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            IAsyncCursor<StockModel> product =
                await _stockCollection.FindAsync(
                    (stock) => stock.ProductId == productId,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

            return await product.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task IncreaseStockAsync(IncreaseStockModel increaseStockModel, CancellationToken cancellationToken = default)
        {
            if (increaseStockModel is null)
            {
                throw new ArgumentNullException(nameof(increaseStockModel));
            }

            StockModel stock = await GetStockAsync(increaseStockModel.ProductId, cancellationToken).ConfigureAwait(false);
            if (stock is null)
            {
                stock = new StockModel(increaseStockModel.ProductId, increaseStockModel.Quantity);
            }
            else
            {
                stock.Quantity += increaseStockModel.Quantity;
            }

            await _stockCollection.ReplaceOneAsync(
                (dbStock) => dbStock.ProductId == increaseStockModel.ProductId,
                stock,
                new ReplaceOptions() { IsUpsert = true },
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
