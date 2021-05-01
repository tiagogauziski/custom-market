using System.Threading;
using System.Threading.Tasks;
using Product.Application.Event.Common;
using Product.Infrastructure.EventStore.Abstractions;

namespace Product.Infrastructure.EventStore.MongoDB
{
    /// <summary>
    /// Defines a MongoDB implementation of <see cref="IEventStoreRepository"/>.
    /// </summary>
    public class EventStoreRepository :
        IEventStoreRepository
    {
        /// <inheritdoc />
        public Task SaveEvent(EventBase systemEvent, CancellationToken cancellation)
        {
            return Task.CompletedTask;
        }
    }
}
