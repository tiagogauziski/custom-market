using System.Collections.Generic;

namespace Product.Application.Common
{
    /// <summary>
    /// Result pattern interface.
    /// </summary>
    /// <typeparam name="T">Data result type.</typeparam>
    public interface IResult<T>
    {
        /// <summary>
        /// Gets a value indicating whether it was executed successfully.
        /// </summary>
        bool IsSuccessful { get; }

        /// <summary>
        /// Gets a list of errors.
        /// </summary>
        IEnumerable<string> Errors { get; }

        /// <summary>
        /// Gets the returning data.
        /// </summary>
        T Data { get; }
    }
}
