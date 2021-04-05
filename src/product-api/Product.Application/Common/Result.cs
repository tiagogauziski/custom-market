using System.Collections.Generic;

namespace Product.Application.Common
{
    /// <summary>
    /// Abstract implementation of <see cref="IResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">Result data type.</typeparam>
    public abstract class Result<T> : IResult<T>
    {
        /// <inheritdoc/>
        public abstract bool IsSuccessful { get; }

        /// <inheritdoc/>
        public abstract IEnumerable<string> Errors { get; }

        /// <inheritdoc/>
        public abstract T Data { get; }
    }
}
