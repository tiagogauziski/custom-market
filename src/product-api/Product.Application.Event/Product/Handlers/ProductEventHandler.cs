using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Event.Product.Events;
using Product.Infrastructure.EventStore.Abstractions;

namespace Product.Application.Event.Product.Handlers
{
    /// <summary>
    /// Implements <see cref="INotificationHandler{TNotification}"/> of product related events.
    /// </summary>
    public class ProductEventHandler :
        INotificationHandler<ProductCreatedEvent>,
        INotificationHandler<ProductUpdatedEvent>,
        INotificationHandler<ProductDeletedEvent>
    {
        private readonly ILogger<ProductEventHandler> _logger;
        private readonly IEventStoreRepository _eventStoreRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductEventHandler"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="eventStoreRepository">Event store repository.</param>
        public ProductEventHandler(
            ILogger<ProductEventHandler> logger,
            IEventStoreRepository eventStoreRepository)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _eventStoreRepository = eventStoreRepository ?? throw new System.ArgumentNullException(nameof(eventStoreRepository));
        }

        /// <inheritdoc/>
        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Product {new} has been created.", notification.New);

            await _eventStoreRepository.SaveEventAsync(notification, cancellationToken);
        }

        /// <inheritdoc />
        public async Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Product {new} has been modified.", notification.New, notification.Old);

            await _eventStoreRepository.SaveEventAsync(notification, cancellationToken);
        }

        /// <inheritdoc />
        public async Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Product {old} has deleted.", notification.Old);

            await _eventStoreRepository.SaveEventAsync(notification, cancellationToken);
        }
    }
}
