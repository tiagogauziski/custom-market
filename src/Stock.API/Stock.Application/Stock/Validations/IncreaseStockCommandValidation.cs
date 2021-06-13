using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Stock.Application.Stock.Command;

namespace Stock.Application.Stock.Validations
{
    /// <summary>
    /// Defines the validation rules for <see cref="IncreaseStockCommand"/>.
    /// </summary>
    public class IncreaseStockCommandValidation
        : AbstractValidator<IncreaseStockCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IncreaseStockCommandValidation"/> class.
        /// </summary>
        public IncreaseStockCommandValidation()
        {
            RuleFor(command => command.ProductId)
                .NotEmpty().WithMessage($"{nameof(IncreaseStockCommand.ProductId)} cannot be empty.");

            RuleFor(command => command.Quantity)
                .GreaterThan(0).WithMessage($"{nameof(IncreaseStockCommand.Quantity)} cannot be a negative value.");
        }
    }
}
