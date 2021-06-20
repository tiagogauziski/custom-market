using System;
using System.Threading;
using System.Threading.Tasks;
using Stock.Domain.Models;

namespace Stock.Infrastructure.Database.Abstractions
{
    /// <summary>
    /// Abstraction that represents saving stock events into data source.
    /// </summary>
    public interface IStockHistoryRepository
    {
        /// <summary>
        /// Register a increase stock event into the datasource.
        /// </summary>
        /// <param name="increaseStockModel">Increase stock model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A unique identifier of the event.</returns>
        Task IncreaseStockEventAsync(StockHistoryModel increaseStockModel, CancellationToken cancellationToken);

        /// <summary>
        /// Register a decrease stock event into the datasource.
        /// </summary>
        /// <param name="decreaseStockModel">Decrease stock model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A unique identifier of the event.</returns>
        Task DecreaseStockEventAsync(StockHistoryModel decreaseStockModel, CancellationToken cancellationToken);
    }
}
