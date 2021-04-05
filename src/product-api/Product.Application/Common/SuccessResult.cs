using System.Collections.Generic;

namespace Product.Application.Common
{
    /// <summary>
    /// Successful result implementation of <see cref="Result{T}"/>.
    /// </summary>
    /// <typeparam name="T">Data return type.</typeparam>
    public class SuccessResult<T>
        : Result<T>
    {
        private readonly T _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessResult{T}"/> class.
        /// </summary>
        /// <param name="data">Result data.</param>
        public SuccessResult(T data)
        {
            _data = data;
        }

        /// <inheritdoc/>
        public override bool IsSuccessful => true;

        /// <inheritdoc/>
        public override List<string> Errors => new List<string>();

        /// <inheritdoc/>
        public override T Data => _data;
    }
}
