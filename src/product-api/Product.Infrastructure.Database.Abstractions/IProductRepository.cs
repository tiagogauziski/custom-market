using System;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Infrastructure.Database
{
    /// <summary>
    /// Interface that contains database operations for product model.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Create a product.
        /// </summary>
        /// <param name="product">Product model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task CreateAsync(Models.Product product, CancellationToken cancellationToken);

        /// <summary>
        /// Update a product.
        /// </summary>
        /// <param name="product">Product model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(Models.Product product, CancellationToken cancellationToken);

        /// <summary>
        /// Delete a product.
        /// </summary>
        /// <param name="product">Product model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteAsync(Models.Product product, CancellationToken cancellationToken);

        /// <summary>
        /// Query product based on product id.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Product model.</returns>
        Task<Models.Product> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
