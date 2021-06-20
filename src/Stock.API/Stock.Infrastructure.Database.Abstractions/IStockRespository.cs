using System;
using System.Threading;
using System.Threading.Tasks;
using Stock.Domain.Models;

namespace Stock.Infrastructure.Database.Abstractions
{
    /// <summary>
    /// Interface to persist the current stock level into the datasouce.
    /// </summary>
    public interface IStockRespository
    {
        /// <summary>
        /// Increase the current stock level.
        /// </summary>
        /// <param name="increaseStockModel">Increase stock model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task.</returns>
        Task IncreaseStockAsync(IncreaseStockModel increaseStockModel, CancellationToken cancellationToken);

        /// <summary>
        /// Decrease the current stock level.
        /// </summary>
        /// <param name="decreaseStockModel">Decrease stock model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task.</returns>
        Task DecreaseStockAsync(DecreaseStockModel decreaseStockModel, CancellationToken cancellationToken);

        /// <summary>
        /// Get the current level of the stock for a given product id.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Current stock level.</returns>
        Task<StockModel> GetStockAsync(Guid productId, CancellationToken cancellationToken);
    }
}
