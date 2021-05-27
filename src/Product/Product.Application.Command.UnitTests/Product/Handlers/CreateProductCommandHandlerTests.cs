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

namespace Product.Application.UnitTests.Product.Handlers
{
    public class CreateProductCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly CreateProductCommandHandler _commandHandler;

        public CreateProductCommandHandlerTests()
        {
            _mocker = new AutoMocker();

            _commandHandler = _mocker.CreateInstance<CreateProductCommandHandler>();
        }

        [Fact]
        public async Task CreateProductCommand_WhenInvalidName_Failure()
        {
            // Arrange
            var command = new CreateProductCommand();

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<ModelValidationResult<Guid>>(result);
            Assert.False(result.IsSuccessful);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public async Task CreateProductCommand_WhenDuplicatedNameBrand_Failure()
        {
            // Arrange
            _mocker.GetMock<IProductRepository>()
                .Setup(m => m.GetByNameBrandAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Models.Product());

            var command = new CreateProductCommand()
            {
                Name = "Name",
                Brand = "Brand",
                Description = "Description",
                Price = 100.12,
                ProductCode = "ProductCode"
            };

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<ConflictResult<Guid>>(result);
            Assert.False(result.IsSuccessful);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public async Task CreateProductCommand_AllProperties_Success()
        {
            // Arrange
            var command = new CreateProductCommand()
            {
                Name = "Name",
                Brand = "Brand",
                Description = "Description",
                Price = 100.12,
                ProductCode = "ProductCode"
            };

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

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
            Models.Product repositoryModel = null;
            _mocker.GetMock<IProductRepository>()
                .Setup(m => m.CreateAsync(It.IsAny<Models.Product>(), It.IsAny<CancellationToken>()))
                .Callback((Models.Product product, CancellationToken cancellationToken) =>
                {
                    repositoryModel = product;
                });

            var command = new CreateProductCommand()
            {
                Name = "Name",
                Brand = "Brand",
                Description = "Description",
                Price = 100.12,
                ProductCode = "ProductCode"
            };

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(command.Brand, repositoryModel.Brand);
            Assert.Equal(command.Description, repositoryModel.Description);
            Assert.Equal(command.Name, repositoryModel.Name);
            Assert.Equal(command.Price, repositoryModel.Price);
            Assert.Equal(command.ProductCode, repositoryModel.ProductCode);
        }
    }
}
