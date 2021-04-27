using FluentValidation;
using Product.Application.Command.Product.Commands;

namespace Product.Application.Command.Product.Validations
{
    /// <summary>
    /// Defines the validation rules for <see cref="CreateProductCommand"/>.
    /// </summary>
    public class CreateProductCommandValidation
        : AbstractValidator<CreateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductCommandValidation"/> class.
        /// </summary>
        public CreateProductCommandValidation()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage($"{nameof(CreateProductCommand.Name)} cannot be empty.");

            RuleFor(command => command.Brand)
                .NotEmpty().WithMessage($"{nameof(CreateProductCommand.Brand)} cannot be empty.");
        }
    }
}
