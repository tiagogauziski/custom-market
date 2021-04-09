using System;
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
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task CreateAsync(Models.Product product);

        /// <summary>
        /// Update a product.
        /// </summary>
        /// <param name="product">Product model.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(Models.Product product);

        /// <summary>
        /// Delete a product.
        /// </summary>
        /// <param name="product">Product model.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteAsync(Models.Product product);

        /// <summary>
        /// Query product based on product id.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <returns>Product model.</returns>
        Task<Models.Product> GetByIdAsync(Guid id);
    }
}
