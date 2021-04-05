using MediatR;

namespace Product.Application.Events
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
            this.New = product;
        }

        /// <summary>
        /// Gets or sets the created product.
        /// </summary>
        public Models.Product New { get; set; }
    }
}
