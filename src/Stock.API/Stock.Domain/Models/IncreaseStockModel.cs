using System;

namespace Stock.Domain.Models
{
    /// <summary>
    /// Model to represent a increase in the stock level.
    /// </summary>
    public class IncreaseStockModel
    {
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public long Quantity { get; set; }
    }
}
