using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Product.Infrastructure.Database.MongoDB.Settings;

namespace Product.Infrastructure.Database.MongoDB.Repositories
{
    /// <summary>
    /// MongoDB implementation of <see cref="IProductRepository"/>.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<Models.Product> _products;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="mongoClient">MongoDB database client.</param>
        /// <param name="productDatabaseSettings">Product database settings.</param>
        public ProductRepository(
            MongoClient mongoClient,
            IProductDatabaseSettings productDatabaseSettings)
        {
            _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));

            IMongoDatabase database = _mongoClient.GetDatabase(productDatabaseSettings.DatabaseName);

            _products = database.GetCollection<Models.Product>("product");
        }

        /// <inheritdoc/>
        public async Task CreateAsync(Models.Product product, CancellationToken cancellationToken)
        {
            await _products.InsertOneAsync(product, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Models.Product product, CancellationToken cancellationToken)
        {
            await _products.DeleteOneAsync((dbProduct) => dbProduct.Id == product.Id, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Models.Product> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            IAsyncCursor<Models.Product> product =
                await _products.FindAsync(
                    (product) => product.Id == id,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

            return product.FirstOrDefault(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Models.Product> GetByNameBrandAsync(string name, string brand, CancellationToken cancellationToken)
        {
            IAsyncCursor<Models.Product> product =
               await _products.FindAsync(
                   (product) => product.Name == name && product.Brand == brand,
                   cancellationToken: cancellationToken).ConfigureAwait(false);

            return product.FirstOrDefault(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Models.Product product, CancellationToken cancellationToken)
        {
            await _products.ReplaceOneAsync(
                (dbProduct) => dbProduct.Id == product.Id,
                product,
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
