using System;
using MediatR;
using Stock.Application.Product.Contracts.V1;
using Stock.Application.Shared;

namespace Stock.Application.Stock.Events
{
    /// <summary>
    /// Event representing an increase in the stock for a given product.
    /// </summary>
    public class StockDecreasedEvent : EventBase, INotification
    {
        /// <summary>
        /// Gets or sets the unique identifier for the event.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public ProductResponse Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public long Quantity { get; set; }
    }
}
