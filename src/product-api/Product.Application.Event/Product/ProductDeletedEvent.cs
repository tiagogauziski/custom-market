using MediatR;

namespace Product.Application.Event.Product
{
    /// <summary>
    /// Event payload when a new product is created.
    /// </summary>
    public class ProductDeletedEvent
        : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDeletedEvent"/> class.
        /// </summary>
        public ProductDeletedEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDeletedEvent"/> class.
        /// </summary>
        /// <param name="product">Product model.</param>
        public ProductDeletedEvent(Models.Product product)
        {
            Old = product;
        }

        /// <summary>
        /// Gets or sets the created product.
        /// </summary>
        public Models.Product Old { get; set; }
    }
}
