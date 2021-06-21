using System;
using FluentResults;
using MediatR;

namespace Stock.Application.Stock.Commands
{
    /// <summary>
    /// Defines a command to increase the stock .
    /// </summary>
    public class DecreaseStockCommand : IRequest<Result<Guid>>
    {
        /// <summary>
        /// Gets or sets product id.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product to increase the stock.
        /// </summary>
        public long Quantity { get; set; }
    }
}
