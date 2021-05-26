using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Product.Infrastructure.Database.EntityFramework.Repositories
{
    public class ProductRepository
        : IProductRepository
    {
        private readonly ProductContext _productContext;

        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public async Task CreateAsync(Models.Product product, CancellationToken cancellationToken)
        {
            await _productContext.AddAsync(product, cancellationToken);
            await _productContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Models.Product product, CancellationToken cancellationToken)
        {
            var dbModel = _productContext.Products.FirstOrDefault(i => i.Id == product.Id);

            _productContext.Products.Remove(dbModel);
            await _productContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Models.Product> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await _productContext.Products.FindAsync(new object[] { id }, cancellationToken);

        /// <inheritdoc />
        public async Task<Models.Product> GetByNameBrandAsync(string name, string brand, CancellationToken cancellationToken)
        {
            return await _productContext.Products
                .Where(product => product.Name.Equals(name) && product.Brand.Equals(brand))
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Models.Product product, CancellationToken cancellationToken)
        {
            _productContext.Products.Update(product);
            await _productContext.SaveChangesAsync();
        }
    }
}
