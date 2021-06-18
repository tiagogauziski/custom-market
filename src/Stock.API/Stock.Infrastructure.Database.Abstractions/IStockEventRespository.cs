using System;
using System.Threading.Tasks;
using Stock.Domain.Models;

namespace Stock.Infrastructure.Database.Abstractions
{
    /// <summary>
    /// Abstraction that represents saving stock events into data source.
    /// </summary>
    public interface IStockEventRespository
    {
        /// <summary>
        /// Register a increase stock event into the datasource.
        /// </summary>
        /// <param name="increaseStockModel">Increase stock model.</param>
        /// <returns>A unique identifier of the event.</returns>
        Task<Guid> IncreaseStockEvent(IncreaseStockModel increaseStockModel);

        /// <summary>
        /// Register a decrease stock event into the datasource.
        /// </summary>
        /// <param name="decreaseStockModel">Decrease stock model.</param>
        /// <returns>A unique identifier of the event.</returns>
        Task<Guid> DecreaseStockEvent(DecreaseStockModel decreaseStockModel);
    }
}
