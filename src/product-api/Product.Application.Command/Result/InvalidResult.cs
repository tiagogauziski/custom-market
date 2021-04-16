using System.Collections.Generic;

namespace Product.Application.Command.Result
{
    /// <summary>
    /// Invalid result implementation of <see cref="Result{T}"/>.
    /// </summary>
    /// <typeparam name="T">Data return type.</typeparam>
    public class InvalidResult<T> : Result<T>
    {
        private readonly string[] _errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidResult{T}"/> class.
        /// </summary>
        /// <param name="error">Result error.</param>
        public InvalidResult(string error)
        {
            _errors = new string[] { error };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidResult{T}"/> class.
        /// </summary>
        /// <param name="errors">Result errors.</param>
        public InvalidResult(string[] errors)
        {
            _errors = errors;
        }

        /// <inheritdoc/>
        public override bool IsSuccessful => false;

        /// <inheritdoc/>
        public override List<string> Errors => new List<string>(_errors);

        /// <inheritdoc/>
        public override T Data => default;
    }
}
