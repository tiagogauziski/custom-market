using MediatR;

namespace Product.Application.Event.Product
{
    /// <summary>
    /// Event payload when a new product is created.
    /// </summary>
    public class ProductCreatedEvent
        : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCreatedEvent"/> class.
        /// </summary>
        public ProductCreatedEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCreatedEvent"/> class.
        /// </summary>
        /// <param name="product">Product model.</param>
        public ProductCreatedEvent(Models.Product product)
        {
            New = product;
        }

        /// <summary>
        /// Gets or sets the created product.
        /// </summary>
        public Models.Product New { get; set; }
    }
}
