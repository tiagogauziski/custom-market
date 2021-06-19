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
using Stock.Application.Stock.Command;
using Stock.Application.Stock.Validations;

namespace Stock.Application.Stock.CommandHandlers
{
    /// <summary>
    /// Handler for <see cref="IncreaseStockCommand"/> action.
    /// </summary>
    public class IncreaseStockCommandHandler : IRequestHandler<IncreaseStockCommand, Result<Guid>>
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="IncreaseStockCommandHandler"/> class.
        /// </summary>
        /// <param name="productService">Product service.</param>
        public IncreaseStockCommandHandler(IProductService productService) => _productService = productService ?? throw new ArgumentNullException(nameof(productService));

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

            return Result.Ok(Guid.NewGuid());
        }
    }
}
