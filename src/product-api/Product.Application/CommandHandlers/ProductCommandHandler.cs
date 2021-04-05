using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Commands.Products;
using Product.Application.Common;
using Product.Application.Events;

namespace Product.Application.CommandHandlers
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"/> for product commands.
    /// </summary>
    public class ProductCommandHandler
        : IRequestHandler<CreateProductCommand, IResult<Guid>>
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCommandHandler"/> class.
        /// </summary>
        /// <param name="mediator">Mediator service.</param>
        public ProductCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <inheritdoc/>
        public Task<IResult<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return Task.FromResult<IResult<Guid>>(new InvalidResult<Guid>($"{nameof(request.Name)} cannot be null or empty."));
            }

            var product = new Models.Product
            {
                Id = Guid.NewGuid(),
            };

            _mediator.Publish(new ProductCreatedEvent(product), cancellationToken);

            return Task.FromResult<IResult<Guid>>(new SuccessResult<Guid>(product.Id));
        }
    }
}
