using System;

namespace Product.Application.Query.Product.Responses
{
    /// <summary>
    /// Determines product model view.
    /// </summary>
    public class ProductResponseModel
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets product brand name.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the product code.
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        public string Description { get; set; }
    }
}
