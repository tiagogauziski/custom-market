using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Product.Application.Command.Product.Commands;
using Product.Application.Command.Product.Validations;
using Product.Application.Command.Result;
using Product.Application.Event.Product;
using Product.Infrastructure.Database;

namespace Product.Application.Command.Product.Handlers
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"/> for product commands.
    /// </summary>
    public class ProductCommandHandler :
        IRequestHandler<CreateProductCommand, IResult<Guid>>,
        IRequestHandler<UpdateProductCommand, IResult<Guid>>
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
            var validation = new CreateProductCommandValidation();

            ValidationResult validationResult = await validation.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new ModelValidationResult<Guid>(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            if (await _productRepository.GetByNameBrandAsync(request.Name, request.Brand, cancellationToken).ConfigureAwait(false) is not null)
            {
                return new ConflictResult<Guid>("Duplicate product. Please provide an unique product entry.");
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

        /// <inheritdoc />
        public async Task<IResult<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validation = new UpdateProductCommandValidation();

            ValidationResult validationResult = await validation.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new ModelValidationResult<Guid>(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            if (await _productRepository.GetByNameBrandAsync(request.Name, request.Brand, cancellationToken).ConfigureAwait(false) is not null)
            {
                return new ConflictResult<Guid>("Duplicate product. Please provide an unique product entry.");
            }

            var existingProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);

            if (existingProduct is null)
            {
                return new NotFoundResult<Guid>("Unable to find the product.");
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

            await _productRepository.UpdateAsync(product, cancellationToken);

            await _mediator.Publish(new ProductUpdatedEvent(existingProduct, product), cancellationToken);

            return new SuccessResult<Guid>(product.Id);
        }
    }
}
