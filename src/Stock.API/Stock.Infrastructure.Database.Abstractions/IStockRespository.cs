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
        /// <returns>A task.</returns>
        Task IncreaseStockAsync(IncreaseStockModel increaseStockModel);

        /// <summary>
        /// Decrease the current stock level.
        /// </summary>
        /// <param name="decreaseStockModel">Decrease stock model.</param>
        /// <returns>A task.</returns>
        Task DecreaseStockAsync(DecreaseStockModel decreaseStockModel);
    }
}
