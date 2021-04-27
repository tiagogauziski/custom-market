using System.Collections.Generic;

namespace Product.Application.Command.Result
{
    /// <summary>
    /// Entry not found result implementation of <see cref="InvalidResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">Result data type.</typeparam>
    public class NotFoundResult<T>
        : InvalidResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundResult{T}"/> class.
        /// </summary>
        /// <param name="error">Model validation error.</param>
        public NotFoundResult(string error)
            : base(error)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundResult{T}"/> class.
        /// </summary>
        /// <param name="errors">Model validation errors.</param>
        public NotFoundResult(IEnumerable<string> errors)
            : base(errors)
        {
        }
    }
}
