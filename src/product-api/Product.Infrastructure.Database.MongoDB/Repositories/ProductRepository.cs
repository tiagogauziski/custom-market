using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Database.MongoDB.Repositories
{
    /// <summary>
    /// MongoDB implementation of <see cref="IProductRepository"/>.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        /// <inheritdoc/>
        public Task CreateAsync(Models.Product product)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Models.Product product)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<Models.Product> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Models.Product product)
        {
            throw new NotImplementedException();
        }
    }
}
