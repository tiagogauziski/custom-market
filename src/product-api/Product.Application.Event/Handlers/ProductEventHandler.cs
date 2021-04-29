using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Event.Product;

namespace Product.Application.Event.Handlers
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductEventHandler"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        public ProductEventHandler(ILogger<ProductEventHandler> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Product {new} has been created.", notification.New);

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Product {new} has been modified.", notification.New, notification.Old);

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Product {new} has deleted.", notification.Old);

            return Task.CompletedTask;
        }
    }
}
