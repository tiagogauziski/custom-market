using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Product.Application.Command.Handlers;
using Product.Application.Command.Products;
using Product.Application.Command.Result;
using Xunit;

namespace Product.Application.UnitTests
{
    public class ProductCommandHandlerTests
    {
        [Fact]
        public async Task CreateProductCommand_WhenInvalidName_Failure()
        {
            // Arrange
            var mediatorMock = Mock.Of<IMediator>();

            var commandHandler = new ProductCommandHandler(mediatorMock);
            var command = new CreateProductCommand();

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<InvalidResult<Guid>>(result);
            Assert.False(result.IsSuccessful);
            Assert.Single(result.Errors);
        }

        [Fact]
        public async Task CreateProductCommand_AllProperties_Success()
        {
            // Arrange
            var mediatorMock = Mock.Of<IMediator>();

            var commandHandler = new ProductCommandHandler(mediatorMock);
            var command = new CreateProductCommand()
            {
                Name = "Name",
                Brand = "Brand",
                Description = "Description",
                Price = 100.12,
                ProductCode = "ProductCode"
            };

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessResult<Guid>>(result);
            Assert.True(result.IsSuccessful);
            Assert.Empty(result.Errors);

            Assert.NotEqual(Guid.Empty, result.Data);
        }
    }
}
