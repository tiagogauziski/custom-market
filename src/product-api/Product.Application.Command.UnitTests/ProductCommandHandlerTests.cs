using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Product.Application.Command.Product.Commands;
using Product.Application.Command.Product.Handlers;
using Product.Application.Command.Result;
using Product.Infrastructure.Database;
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
            var repositoryMock = new Mock<IProductRepository>();

            var commandHandler = new ProductCommandHandler(mediatorMock, repositoryMock.Object);
            var command = new CreateProductCommand();

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<ModelValidationResult<Guid>>(result);
            Assert.False(result.IsSuccessful);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public async Task CreateProductCommand_AllProperties_Success()
        {
            // Arrange
            var mediatorMock = Mock.Of<IMediator>();
            var repositoryMock = new Mock<IProductRepository>();

            var commandHandler = new ProductCommandHandler(mediatorMock, repositoryMock.Object);
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

        [Fact]
        public async Task CreateProductCommand_VerifyCommandModelMapping()
        {
            // Arrange
            var mediatorMock = Mock.Of<IMediator>();
            var repositoryMock = new Mock<IProductRepository>();
            Models.Product repositoryModel = null;
            repositoryMock
                .Setup(m => m.CreateAsync(It.IsAny<Models.Product>(), It.IsAny<CancellationToken>()))
                .Callback((Models.Product product, CancellationToken cancellationToken) =>
                {
                    repositoryModel = product;
                });

            var commandHandler = new ProductCommandHandler(mediatorMock, repositoryMock.Object);
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
            Assert.Equal(command.Brand, repositoryModel.Brand);
            Assert.Equal(command.Description, repositoryModel.Description);
            Assert.Equal(command.Name, repositoryModel.Name);
            Assert.Equal(command.Price, repositoryModel.Price);
            Assert.Equal(command.ProductCode, repositoryModel.ProductCode);
        }
    }
}
