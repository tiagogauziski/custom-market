using System;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
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
        private IMongoCollection<Models.Product> _products;

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

            var database = _mongoClient.GetDatabase(productDatabaseSettings.DatabaseName);

            _products = database.GetCollection<Models.Product>("products");
        }

        /// <inheritdoc/>
        public async Task CreateAsync(Models.Product product)
        {
            await _products.InsertOneAsync(product);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Models.Product product)
        {
            await _products.DeleteOneAsync((product) => product.Id == product.Id);
        }

        /// <inheritdoc/>
        public async Task<Models.Product> GetByIdAsync(Guid id)
        {
            return (await _products.FindAsync((product) => product.Id == id)).FirstOrDefault();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Models.Product product)
        {
            await _products.ReplaceOneAsync((product) => product.Id == product.Id, product);
        }
    }
}
