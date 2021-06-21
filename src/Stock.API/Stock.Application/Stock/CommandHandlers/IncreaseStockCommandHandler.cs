using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using FluentValidation.Results;
using MediatR;
using Stock.Application.Product.Contracts.V1;
using Stock.Application.Product.Errors;
using Stock.Application.Product.Service;
using Stock.Application.Stock.Commands;
using Stock.Application.Stock.Events;
using Stock.Application.Stock.Validations;
using Stock.Domain.Models;
using Stock.Infrastructure.Database.Abstractions;

namespace Stock.Application.Stock.CommandHandlers
{
    /// <summary>
    /// Handler for <see cref="IncreaseStockCommand"/> action.
    /// </summary>
    public class IncreaseStockCommandHandler : IRequestHandler<IncreaseStockCommand, Result<Guid>>
    {
        private readonly IProductService _productService;
        private readonly IStockRepository _stockRespository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="IncreaseStockCommandHandler"/> class.
        /// </summary>
        /// <param name="productService">Product service.</param>
        /// <param name="stockRespository">Stock repository.</param>
        /// <param name="mediator">Mediator service.</param>
        public IncreaseStockCommandHandler(
            IProductService productService,
            IStockRepository stockRespository,
            IMediator mediator)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _stockRespository = stockRespository ?? throw new ArgumentNullException(nameof(stockRespository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <inheritdoc/>
        public async Task<Result<Guid>> Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
        {
            // Validate command
            var validation = new IncreaseStockCommandValidation();

            ValidationResult validationResult = await validation.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Merge(validationResult.Errors.Select(validationError => Result.Fail(validationError.ErrorMessage)).ToArray());
            }

            // Does the product exists?
            Result<ProductResponse> productResult = await _productService.GetProductByIdAsync(request.ProductId, cancellationToken).ConfigureAwait(false);
            if (productResult.HasError<ProductNotFoundError>())
            {
                return Result.Fail(productResult.Errors.First());
            }
            else if (productResult.IsFailed)
            {
                return Result.Fail("Unable to get product details.");
            }

            // Add product quantity into stock.
            await _stockRespository.IncreaseStockAsync(
                new IncreaseStockModel()
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                },
                cancellationToken).ConfigureAwait(false);

            StockIncreasedEvent stockIncreasedEvent = new StockIncreasedEvent()
            {
                Id = Guid.NewGuid(),
                Product = productResult.Value,
                Quantity = request.Quantity,
            };

            await _mediator.Publish(stockIncreasedEvent).ConfigureAwait(false);

            return Result.Ok(stockIncreasedEvent.Id);
        }
    }
}
