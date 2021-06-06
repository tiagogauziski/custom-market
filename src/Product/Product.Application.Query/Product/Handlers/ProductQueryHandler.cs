using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Query.Product.Queries;
using Product.Application.Query.Product.Responses;
using Product.Infrastructure.Database;

namespace Product.Application.Query.Product.Handlers
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"/> for product commands.
    /// </summary>
    public class ProductQueryHandler
        : IRequestHandler<GetProductByIdQuery, ProductResponseModel>
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductQueryHandler"/> class.
        /// </summary>
        /// <param name="productRepository">Product repository.</param>
        public ProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        /// <inheritdoc />
        public async Task<ProductResponseModel> Handle(GetProductByIdQuery getProductByIdQuery, CancellationToken cancellationToken)
        {
            if (getProductByIdQuery is null)
            {
                throw new ArgumentNullException(nameof(getProductByIdQuery));
            }

            Models.Product productModel = await _productRepository.GetByIdAsync(getProductByIdQuery.Id, cancellationToken).ConfigureAwait(false);

            if (productModel is null)
            {
                return null;
            }

            return new ProductResponseModel()
            {
                Id = productModel.Id,
                Brand = productModel.Brand,
                Description = productModel.Description,
                Name = productModel.Name,
                Price = productModel.Price,
                ProductCode = productModel.ProductCode,
            };
        }
    }
}
