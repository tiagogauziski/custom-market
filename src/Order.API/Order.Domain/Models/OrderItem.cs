using System;

namespace Order.Domain.Models
{
    /// <summary>
    /// Model that represents an order item. Includes details of the product selected and quantity.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Gets or sets the item sequential number.
        /// </summary>
        public int ItemNumber { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int Quantity { get; set; }
    }
}
