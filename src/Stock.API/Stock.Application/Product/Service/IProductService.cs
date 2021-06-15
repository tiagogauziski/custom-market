using System;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using Stock.Application.Product.Contracts.V1;

namespace Stock.Application.Product.Service
{
    /// <summary>
    /// Defines a service to consume common Product API endpoints.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get a product details using product id.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Product response model.</returns>
        Task<Result<ProductResponse>> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken);
    }
}
