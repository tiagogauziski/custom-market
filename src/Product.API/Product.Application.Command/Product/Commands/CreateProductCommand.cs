using System;
using MediatR;
using Product.Application.Command.Result;

namespace Product.Application.Command.Product.Commands
{
    /// <summary>
    /// Defines a command to create a product into the database.
    /// </summary>
    public class CreateProductCommand
        : IRequest<IResult<Guid>>
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
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string Description { get; set; }
    }
}
