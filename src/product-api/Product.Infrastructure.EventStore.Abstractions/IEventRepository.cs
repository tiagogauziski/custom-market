using System.Threading;
using System.Threading.Tasks;
using Product.Application.Event.Common;

namespace Product.Infrastructure.EventStore.Abstractions
{
    public interface IEventRepository
    {
        Task SaveEvent(EventBase systemEvent, CancellationToken cancellation);
    }
}
