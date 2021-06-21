using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Application.Stock.Events;
using Stock.Domain.Models;
using Stock.Infrastructure.Database.Abstractions;

namespace Stock.Application.Stock.EventHandlers
{
    /// <summary>
    /// Event handler for stock events.
    /// </summary>
    public class StockEventHandler :
        INotificationHandler<StockIncreasedEvent>,
        INotificationHandler<StockDecreasedEvent>
    {
        private readonly ILogger<StockEventHandler> _logger;
        private readonly IStockHistoryRepository _stockHistoryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockEventHandler"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="stockHistoryRepository">Stock history repository.</param>
        public StockEventHandler(
            ILogger<StockEventHandler> logger,
            IStockHistoryRepository stockHistoryRepository)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _stockHistoryRepository = stockHistoryRepository ?? throw new System.ArgumentNullException(nameof(stockHistoryRepository));
        }

        /// <inheritdoc />
        public async Task Handle(StockIncreasedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Stock have increase {quantity} for the product {productCode}.", notification.Quantity, notification.Product.ProductCode);

            await _stockHistoryRepository.IncreaseStockEventAsync(
                new StockHistoryModel()
                {
                    Id = notification.Id,
                    DateTimeUtc = notification.DateTimeUtc,
                    EventName = notification.EventName,
                    ProductId = notification.Product.Id,
                    Quantity = notification.Quantity,
                },
                cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task Handle(StockDecreasedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Stock have decreased {quantity} for the product {productCode}.", notification.Quantity, notification.Product.ProductCode);

            await _stockHistoryRepository.DecreaseStockEventAsync(
                new StockHistoryModel()
                {
                    Id = notification.Id,
                    DateTimeUtc = notification.DateTimeUtc,
                    EventName = notification.EventName,
                    ProductId = notification.Product.Id,
                    Quantity = notification.Quantity,
                },
                cancellationToken).ConfigureAwait(false);
        }
    }
}
