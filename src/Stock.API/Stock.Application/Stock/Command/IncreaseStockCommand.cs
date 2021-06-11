using System;

namespace Stock.Application.Stock.Command
{
    /// <summary>
    /// Defines a command to decrease the stock quantity quantity.
    /// </summary>
    public class IncreaseStockCommand
    {
        /// <summary>
        /// Gets or sets product id.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product to increase the stock.
        /// </summary>
        public double Quantity { get; set; }
    }
}
