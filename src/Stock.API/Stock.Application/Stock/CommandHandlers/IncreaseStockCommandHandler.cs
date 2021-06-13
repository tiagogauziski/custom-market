using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using FluentValidation.Results;
using MediatR;
using Stock.Application.Stock.Command;
using Stock.Application.Stock.Validations;

namespace Stock.Application.Stock.CommandHandlers
{
    /// <summary>
    /// Handler for <see cref="IncreaseStockCommand"/> action.
    /// </summary>
    public class IncreaseStockCommandHandler : IRequestHandler<IncreaseStockCommand, Result<Guid>>
    {
        /// <inheritdoc/>
        public async Task<Result<Guid>> Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
        {
            // Validate command
            var validation = new IncreaseStockCommandValidation();

            ValidationResult validationResult = await validation.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Merge(validationResult.Errors.Select(validationError => Result.Fail(validationError.ErrorMessage)).ToArray());
            }

            // Does the product exists?
            return Result.Ok(Guid.NewGuid());
        }
    }
}
