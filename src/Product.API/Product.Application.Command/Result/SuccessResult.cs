using System.Collections.Generic;

namespace Product.Application.Command.Result
{
    /// <summary>
    /// Successful result implementation of <see cref="IResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">Data return type.</typeparam>
    public class SuccessResult<T>
        : IResult<T>
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
        public bool IsSuccessful => true;

        /// <inheritdoc/>
        public IEnumerable<string> Errors => new List<string>();

        /// <inheritdoc/>
        public T Data => _data;
    }
}
