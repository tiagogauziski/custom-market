using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Events;

namespace Product.Application.EventHandlers
{
    /// <summary>
    /// Implements <see cref="INotificationHandler{TNotification}"/> of product related events.
    /// </summary>
    public class ProductEventHandler
        : INotificationHandler<ProductCreatedEvent>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductEventHandler"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        public ProductEventHandler(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Product {notification} has been created.");

            return Task.CompletedTask;
        }
    }
}
