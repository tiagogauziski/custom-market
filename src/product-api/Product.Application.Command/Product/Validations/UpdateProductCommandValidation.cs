using Product.Application.Command.Product.Commands;

namespace Product.Application.Command.Product.Validations
{
    /// <summary>
    /// Defines the validation rules for <see cref="UpdateProductCommand"/>.
    /// </summary>
    public class UpdateProductCommandValidation :
        CreateProductCommandValidation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCommandValidation"/> class.
        /// </summary>
        public UpdateProductCommandValidation()
        {
        }
    }
}
