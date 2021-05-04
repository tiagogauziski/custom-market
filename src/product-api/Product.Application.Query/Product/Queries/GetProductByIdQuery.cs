using System;
using MediatR;
using Product.Application.Query.Product.Responses;

namespace Product.Application.Query.Product.Queries
{
    /// <summary>
    /// Defines the get product by id query parameters.
    /// </summary>
    public class GetProductByIdQuery : IRequest<ProductResponseModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductByIdQuery"/> class.
        /// </summary>
        /// <param name="id">Product id.</param>
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the product id.
        /// </summary>
        public Guid Id { get; private set; }
    }
}
