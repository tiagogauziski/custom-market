using System;

namespace Product.Models
{
    /// <summary>
    /// Defines a product history model.
    /// </summary>
    public class ProductHistory
    {
        /// <summary>
        /// Gets or sets the product history id.
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
        /// Gets or sets the changes being made.
        /// </summary>
        public string Changes { get; set; }

        /// <summary>
        /// Gets or sets the date and time the event happened.
        /// </summary>
        public DateTimeOffset DateTimeUtc { get; set; }
    }
}
