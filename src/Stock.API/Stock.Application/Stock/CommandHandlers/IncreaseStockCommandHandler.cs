using System;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Stock.Application.Stock.Command;

namespace Stock.Application.Stock.CommandHandlers
{
    /// <summary>
    /// Handler for <see cref="IncreaseStockCommand"/> action.
    /// </summary>
    public class IncreaseStockCommandHandler : IRequestHandler<IncreaseStockCommand, Result<Guid>>
    {
        /// <inheritdoc/>
        public Task<Result<Guid>> Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
        {
            // Validate command - is product guid valid?
            if (request.ProductId == Guid.Empty)
            {
                return Task.FromResult(Result.Fail<Guid>("Product Id cannot be empty."));
            }

            // Validate command - is product guid valid?
            if (request.Quantity <= 0)
            {
                return Task.FromResult(Result.Fail<Guid>("Quantity is invalid."));
            }

            // Does the product exists?
            return Task.FromResult(Result.Ok(Guid.NewGuid()));
        }
    }
}
