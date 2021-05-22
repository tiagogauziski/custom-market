using System;
using System.Threading;
using System.Threading.Tasks;
using Product.Models;

namespace Product.Infrastructure.Database.EntityFramework.Repositories
{
    /// <summary>
    /// Entity framework implementation of <see cref="IProductHistoryRepository"/>.
    /// Supports multiple database engines.
    /// </summary>
    public class ProductHistoryRepository
        : IProductHistoryRepository
    {
        private readonly ProductContext _productContext;

        public ProductHistoryRepository(ProductContext expensesContext)
        {
            _productContext = expensesContext;
        }

        /// <inheritdoc />
        public async Task SaveEventAsync(EventBase systemEvent, CancellationToken cancellationToken)
        {
            await _productContext.ProductHistory.AddAsync(new ProductHistory()
            {
                Id = Guid.NewGuid(),
                ProductId = systemEvent.ObjectId,
                Changes = systemEvent.GetChanges(),
                DateTimeUtc = DateTimeOffset.UtcNow,
                EventName = systemEvent.EventName,
            }, cancellationToken);
        }
    }
}
