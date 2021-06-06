using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using Product.Application.Command.Product.Commands;
using Product.Application.Command.Product.Handlers;
using Product.Application.Command.Result;
using Product.Infrastructure.Database;
using Xunit;

namespace Product.Application.Command.UnitTests.Product.Handlers
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly DeleteProductCommandHandler _commandHandler;

        public DeleteProductCommandHandlerTests()
        {
            _mocker = new AutoMocker();

            _commandHandler = _mocker.CreateInstance<DeleteProductCommandHandler>();
        }

        [Fact]
        public async Task UpdateProductCommand_AllProperties_Success()
        {
            // Arrange
            Models.Product existingProduct = new Models.Product()
            {
                Id = Guid.NewGuid(),
                Name = "ExistingName",
                Brand = "ExistingBrand",
                Description = "ExistingDescription",
                Price = 1234,
                ProductCode = "ExistingProductCode"
            };

            _mocker.GetMock<IProductRepository>()
                .Setup(m => m.GetByIdAsync(It.Is<Guid>(p => p == existingProduct.Id), CancellationToken.None))
                .ReturnsAsync(existingProduct);

            var command = new DeleteProductCommand()
            {
                Id = existingProduct.Id,
            };

            // Act
            IResult<bool> result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessResult<bool>>(result);
            Assert.True(result.IsSuccessful);
            Assert.Empty(result.Errors);

            Assert.True(result.Data);
        }

        [Fact]
        public async Task UpdateProductCommand_WhenProductDoesNotExists_Failure()
        {
            // Arrange
            Models.Product existingProduct = new Models.Product()
            {
                Id = Guid.NewGuid()
            };

            _mocker.GetMock<IProductRepository>()
                .Setup(m => m.GetByIdAsync(It.Is<Guid>(p => p == existingProduct.Id), CancellationToken.None))
                .ReturnsAsync(existingProduct);

            var command = new DeleteProductCommand()
            {
                Id = Guid.NewGuid(),
            };

            // Act
            IResult<bool> result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<NotFoundResult<bool>>(result);
            Assert.False(result.IsSuccessful);
            Assert.NotEmpty(result.Errors);
        }
    }
}
