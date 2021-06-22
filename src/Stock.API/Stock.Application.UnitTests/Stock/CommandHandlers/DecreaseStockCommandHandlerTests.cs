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
    public class DecreaseStockCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly DecreaseStockCommandHandler _commandHandler;

        public DecreaseStockCommandHandlerTests()
        {
            _mocker = new AutoMocker();

            _commandHandler = _mocker.CreateInstance<DecreaseStockCommandHandler>();
        }

        [Fact]
        public async Task Handle_InvalidProductId_ShouldReturnError()
        {
            // Arrange
            DecreaseStockCommand decreaseStockCommand = new DecreaseStockCommand()
            {
                ProductId = Guid.Empty,
                Quantity = 1
            };

            // Act
            Result<Guid> result = await _commandHandler.Handle(decreaseStockCommand, CancellationToken.None);

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
            DecreaseStockCommand decreaseStockCommand = new DecreaseStockCommand()
            {
                ProductId = Guid.Empty,
                Quantity = quantity
            };

            // Act
            Result<Guid> result = await _commandHandler.Handle(decreaseStockCommand, CancellationToken.None);

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

            DecreaseStockCommand decreaseStockCommand = new DecreaseStockCommand()
            {
                ProductId = productId,
                Quantity = 1
            };

            // Act
            Result<Guid> result = await _commandHandler.Handle(decreaseStockCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(new ProductNotFoundError().Message, result.Errors.First().Message);
        }

        [Fact]
        public async Task Handle_WhenStockQuantityBelowRequested_ShouldReturnProductOutOfStockError()
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

            DecreaseStockCommand decreaseStockCommand = new DecreaseStockCommand()
            {
                ProductId = productId,
                Quantity = 2
            };

            _mocker.GetMock<IStockRepository>()
                .Setup(stockRepository => stockRepository.GetStockAsync(It.Is<Guid>(id => id == productId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StockModel() { ProductId = productId, Quantity = 1 })
                .Verifiable();

            // Act
            Result<Guid> result = await _commandHandler.Handle(decreaseStockCommand, CancellationToken.None);

            // Assert
            _mocker.Verify();
            Assert.True(result.IsFailed);
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors);
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

            DecreaseStockCommand decreaseStockCommand = new DecreaseStockCommand()
            {
                ProductId = productId,
                Quantity = 1
            };

            _mocker.GetMock<IStockRepository>()
                .Setup(stockRepository => stockRepository.DecreaseStockAsync(It.IsAny<DecreaseStockModel>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            _mocker.GetMock<IStockRepository>()
                .Setup(stockRepository => stockRepository.GetStockAsync(It.Is<Guid>(id => id == productId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StockModel() { ProductId = productId, Quantity = 2 })
                .Verifiable();

            _mocker.GetMock<IMediator>()
                .Setup(mediator => mediator.Publish(
                    It.Is<StockDecreasedEvent>(notification => notification.Product.Id == decreaseStockCommand.ProductId && notification.Quantity == decreaseStockCommand.Quantity),
                    It.IsAny<CancellationToken>()))
                .Verifiable();

            // Act
            Result<Guid> result = await _commandHandler.Handle(decreaseStockCommand, CancellationToken.None);

            // Assert
            _mocker.Verify();
            Assert.False(result.IsFailed);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
        }
    }
}
