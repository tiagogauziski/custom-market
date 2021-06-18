using System.Net;
using FluentResults;

namespace Stock.Application.Product.Errors
{
    /// <summary>
    /// Defines an unexpected response from Product API.
    /// </summary>
    public class ProductUnexpectedResponseError : Error
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUnexpectedResponseError"/> class.
        /// </summary>
        /// <param name="httpStatusCode">Product API status code response.</param>
        public ProductUnexpectedResponseError(HttpStatusCode httpStatusCode)
            : base("Unexpected response from Product API.")
        {
            HttpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Gets the HTTP status code returned from Product API.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }
    }
}
