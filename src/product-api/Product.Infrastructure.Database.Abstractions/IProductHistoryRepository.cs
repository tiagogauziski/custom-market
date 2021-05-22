using System.Threading;
using System.Threading.Tasks;
using Product.Models;

namespace Product.Infrastructure.Database
{
    /// <summary>
    /// Abstraction of repository to save product events into data store.
    /// </summary>
    public interface IProductHistoryRepository
    {
        /// <summary>
        /// Save an product event into data store.
        /// </summary>
        /// <param name="systemEvent">Event being stored.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        Task SaveEventAsync(EventBase systemEvent, CancellationToken cancellationToken);
    }
}
