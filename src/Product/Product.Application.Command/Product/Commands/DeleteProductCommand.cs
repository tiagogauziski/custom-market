using System;
using MediatR;
using Product.Application.Command.Result;

namespace Product.Application.Command.Product.Commands
{
    /// <summary>
    /// Defines a command to delete a product from database.
    /// </summary>
    public class DeleteProductCommand :
        IRequest<IResult<bool>>
    {
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public Guid Id { get; set; }
    }
}
