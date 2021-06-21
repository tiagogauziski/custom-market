using FluentResults;

namespace Stock.Application.Stock.Errors
{
    /// <summary>
    /// Error that occurs when a product is out of stock.
    /// </summary>
    public class ProductOutOfStockError : Error
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductOutOfStockError"/> class.
        /// </summary>
        public ProductOutOfStockError()
            : base("The requested product is out of stock.")
        {
        }
    }
}
