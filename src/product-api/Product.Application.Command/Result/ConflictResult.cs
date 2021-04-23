using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Command.Result
{
    /// <summary>
    /// Confict result implementation of <see cref="InvalidResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">Data return type.</typeparam>
    public class ConflictResult<T> : InvalidResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictResult{T}"/> class.
        /// </summary>
        /// <param name="error">Model validation error.</param>
        public ConflictResult(string error)
            : base(error)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictResult{T}"/> class.
        /// </summary>
        /// <param name="errors">Model validation errors.</param>
        public ConflictResult(IEnumerable<string> errors)
            : base(errors)
        {
        }
    }
}
