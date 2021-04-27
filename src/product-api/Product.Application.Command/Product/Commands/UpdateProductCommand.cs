using System;
using MediatR;

namespace Product.Application.Command.Product.Commands
{
    /// <summary>
    /// Update product implementation of <see cref="IRequestHandler{TRequest, TResponse}"/>.
    /// </summary>
    public class UpdateProductCommand
        : CreateProductCommand
    {
        /// <summary>
        /// Gets or sets product id.
        /// </summary>
        public Guid Id { get; set; }
    }
}
