using FluentValidation;
using Stock.Application.Stock.Commands;

namespace Stock.Application.Stock.Validations
{
    /// <summary>
    /// Defines the validation rules for <see cref="IncreaseStockCommand"/>.
    /// </summary>
    public class DecreaseStockCommandValidation
        : AbstractValidator<DecreaseStockCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecreaseStockCommandValidation"/> class.
        /// </summary>
        public DecreaseStockCommandValidation()
        {
            RuleFor(command => command.ProductId)
                .NotEmpty().WithMessage($"{nameof(IncreaseStockCommand.ProductId)} cannot be empty.");

            RuleFor(command => command.Quantity)
                .GreaterThan(0).WithMessage($"{nameof(IncreaseStockCommand.Quantity)} cannot be a negative value.");
        }
    }
}
