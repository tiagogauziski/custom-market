using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Query.Product;
using Product.Infrastructure.Database;

namespace Product.Application.Query.Handlers
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
            _productRepository = productRepository ?? throw new System.ArgumentNullException(nameof(productRepository));
        }

        /// <inheritdoc />
        public async Task<ProductResponseModel> Handle(GetProductByIdQuery getProductByIdQuery, CancellationToken cancellationToken)
        {
            if (getProductByIdQuery is null)
            {
                throw new System.ArgumentNullException(nameof(getProductByIdQuery));
            }

            var productModel = await _productRepository.GetByIdAsync(getProductByIdQuery.Id, cancellationToken).ConfigureAwait(false);

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
