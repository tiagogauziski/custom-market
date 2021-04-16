using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Command.Product.Commands;
using Product.Application.Command.Result;
using Product.Application.Event.Product;
using Product.Infrastructure.Database;

namespace Product.Application.Command.Product.Handlers
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"/> for product commands.
    /// </summary>
    public class ProductCommandHandler
        : IRequestHandler<CreateProductCommand, IResult<Guid>>
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCommandHandler"/> class.
        /// </summary>
        /// <param name="mediator">Mediator service.</param>
        /// <param name="productRepository">Product repository.</param>
        public ProductCommandHandler(
            IMediator mediator,
            IProductRepository productRepository)
        {
            _mediator = mediator;
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        /// <inheritdoc/>
        public async Task<IResult<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new InvalidResult<Guid>($"{nameof(request.Name)} cannot be null or empty.");
            }

            var product = new Models.Product
            {
                Id = Guid.NewGuid(),
                Brand = request.Brand,
                Description = request.Description,
                Name = request.Name,
                Price = request.Price,
                ProductCode = request.ProductCode,
            };

            await _productRepository.CreateAsync(product, cancellationToken);

            await _mediator.Publish(new ProductCreatedEvent(product), cancellationToken);

            return new SuccessResult<Guid>(product.Id);
        }
    }
}
