using System;
using MediatR;
using Product.Application.Common;

namespace Product.Application.Commands.Products
{
    /// <summary>
    /// Create product implementation of <see cref="IRequestHandler{TRequest, TResponse}"/>.
    /// </summary>
    public class CreateProductCommand : IRequest<IResult<Guid>>
    {
        /// <summary>
        /// Gets or sets product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets product brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets product code.
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// Gets or sets price.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string Description { get; set; }
    }
}
