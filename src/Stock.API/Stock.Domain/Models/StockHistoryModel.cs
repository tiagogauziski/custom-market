using System;

namespace Stock.Domain.Models
{
    /// <summary>
    /// Defiones a stock history model.
    /// </summary>
    public class StockHistoryModel
    {
        /// <summary>
        /// Gets or sets the stock history id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity affected.
        /// </summary>
        public long Quantity { get; set; }

        /// <summary>
        /// Gets or sets the date and time the event happened.
        /// </summary>
        public DateTimeOffset DateTimeUtc { get; set; }
    }
}
