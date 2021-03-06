using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Moq;
using Moq.AutoMock;
using Stock.Application.Product.Contracts.V1;
using Stock.Application.Product.Errors;
using Stock.Application.Product.Service;
using Stock.Application.Stock.CommandHandlers;
using Stock.Application.Stock.Commands;
using Stock.Application.Stock.Events;
using Stock.Domain.Models;
using Stock.Infrastructure.Database.Abstractions;
using Xunit;

namespace Stock.Application.UnitTests.Stock.CommandHandlers
{
    public class IncreaseStockCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly IncreaseStockCommandHandler _commandHandler;

        public IncreaseStockCommandHandlerTests()
        {
            _mocker = new AutoMocker();

            _commandHandler = _mocker.CreateInstance<IncreaseStockCommandHandler>();
        }

        [Fact]
        public async Task Handle_InvalidProductId_ShouldReturnError()
        {
            // Arrange
            IncreaseStockCommand increaseStockCommand = new IncreaseStockCommand()
            {
                ProductId = Guid.Empty,
                Quantity = 1
            };

            // Act
            Result<Guid> result = await _commandHandler.Handle(increaseStockCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Handle_InvalidQuantity_ShouldReturnError(int quantity)
        {
            // Arrange
            IncreaseStockCommand increaseStockCommand = new IncreaseStockCommand()
            {
                ProductId = Guid.Empty,
                Quantity = quantity
            };

            // Act
            Result<Guid> result = await _commandHandler.Handle(increaseStockCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public async Task Handle_ProductNotFound_ShouldReturnError()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            _mocker.GetMock<IProductService>()
                .Setup(productService => productService.GetProductByIdAsync(
                    It.Is<Guid>(id => id == productId),
                    CancellationToken.None))
                .ReturnsAsync(Result.Fail(new ProductNotFoundError()));

            IncreaseStockCommand increaseStockCommand = new IncreaseStockCommand()
            {
                ProductId = productId,
                Quantity = 1
            };

            // Act
            Result<Guid> result = await _commandHandler.Handle(increaseStockCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(new ProductNotFoundError().Message, result.Errors.First().Message);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnEventId()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            _mocker.GetMock<IProductService>()
                .Setup(productService => productService.GetProductByIdAsync(
                    It.Is<Guid>(id => id == productId),
                    CancellationToken.None))
                .ReturnsAsync(Result.Ok(new ProductResponse()
                {
                    Id = productId,
                    Name = "TestProduct",
                    Brand = "TestBrand"
                }));

            IncreaseStockCommand increaseStockCommand = new IncreaseStockCommand()
            {
                ProductId = productId,
                Quantity = 1
            };

            _mocker.GetMock<IStockRepository>()
                .Setup(stockRepository => stockRepository.IncreaseStockAsync(It.IsAny<IncreaseStockModel>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            _mocker.GetMock<IMediator>()
                .Setup(mediator => mediator.Publish(
                    It.Is<StockIncreasedEvent>(notification => notification.Product.Id == increaseStockCommand.ProductId && notification.Quantity == increaseStockCommand.Quantity),
                    It.IsAny<CancellationToken>()))
                .Verifiable();

            // Act
            Result<Guid> result = await _commandHandler.Handle(increaseStockCommand, CancellationToken.None);

            // Assert
            _mocker.Verify();
            Assert.False(result.IsFailed);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
        }
    }
}
