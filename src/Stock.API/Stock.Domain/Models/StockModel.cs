using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Models
{
    /// <summary>
    /// Current stock level representation.
    /// </summary>
    public class StockModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockModel"/> class.
        /// </summary>
        public StockModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockModel"/> class.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="quantity">Quantity.</param>
        public StockModel(Guid productId, long quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product quantity in stock.
        /// </summary>
        public long Quantity { get; set; }
    }
}
