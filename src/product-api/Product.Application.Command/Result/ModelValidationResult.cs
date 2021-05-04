using System.Collections.Generic;

namespace Product.Application.Command.Result
{
    /// <summary>
    /// Model validation result implementation of <see cref="InvalidResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">Data return type.</typeparam>
    public class ModelValidationResult<T> : InvalidResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelValidationResult{T}"/> class.
        /// </summary>
        /// <param name="error">Model validation error.</param>
        public ModelValidationResult(string error)
            : base(error)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelValidationResult{T}"/> class.
        /// </summary>
        /// <param name="errors">Model validation errors.</param>
        public ModelValidationResult(IEnumerable<string> errors)
            : base(errors)
        {
        }
    }
}
