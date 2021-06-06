using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Command.Product.Commands;
using Product.Application.Command.Result;
using Product.Application.Event.Product.Events;
using Product.Infrastructure.Database;

namespace Product.Application.Command.Product.Handlers
{
    /// <summary>
    /// Defines behavior for <see cref="DeleteProductCommand"/>.
    /// </summary>
    public class DeleteProductCommandHandler :
        IRequestHandler<DeleteProductCommand, IResult<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductCommandHandler"/> class.
        /// </summary>
        /// <param name="mediator">Mediator service.</param>
        /// <param name="productRepository">Product repository.</param>
        public DeleteProductCommandHandler(
            IMediator mediator,
            IProductRepository productRepository)
        {
            _mediator = mediator;
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        /// <inheritdoc />
        public async Task<IResult<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Models.Product existingProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
            if (existingProduct is null)
            {
                return new NotFoundResult<bool>("Unable to find the product.");
            }

            await _productRepository.DeleteAsync(existingProduct, cancellationToken);

            await _mediator.Publish(new ProductDeletedEvent(existingProduct), cancellationToken);

            return new SuccessResult<bool>(true);
        }
    }
}
