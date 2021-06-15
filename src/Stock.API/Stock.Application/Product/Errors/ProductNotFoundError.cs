using FluentResults;

namespace Stock.Application.Product.Errors
{
    /// <summary>
    /// Product not found validation error.
    /// </summary>
    public class ProductNotFoundError : Error
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductNotFoundError"/> class.
        /// </summary>
        public ProductNotFoundError()
            : base("Product not found.")
        {
        }
    }
}
