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
    public class UpdateProductCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly UpdateProductCommandHandler _commandHandler;

        public UpdateProductCommandHandlerTests()
        {
            _mocker = new AutoMocker();

            _commandHandler = _mocker.CreateInstance<UpdateProductCommandHandler>();
        }

        [Fact]
        public async Task UpdateProductCommand_WhenInvalidName_Failure()
        {
            // Arrange
            var command = new UpdateProductCommand();

            // Act
            IResult<Guid> result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<ModelValidationResult<Guid>>(result);
            Assert.False(result.IsSuccessful);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public async Task UpdateProductCommand_WhenDuplicatedNameBrand_Failure()
        {
            // Arrange
            Models.Product existingProduct = new Models.Product()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Brand = "Brand",
                Description = "ExistingDescription",
                Price = 1234,
                ProductCode = "ExistingProductCode"
            };

            _mocker.GetMock<IProductRepository>()
                .Setup(m => m.GetByNameBrandAsync(It.Is<string>(p => p == existingProduct.Name), It.Is<string>(p => p == existingProduct.Brand), CancellationToken.None))
                .ReturnsAsync(value: existingProduct);

            _mocker.GetMock<IProductRepository>()
                .Setup(m => m.GetByIdAsync(It.Is<Guid>(p => p == existingProduct.Id), CancellationToken.None))
                .ReturnsAsync(new Models.Product());

            var command = new UpdateProductCommand()
            {
                Id = existingProduct.Id,
                Name = "Name",
                Brand = "Brand",
                Description = "Description",
                Price = 100.12m,
                ProductCode = "ProductCode"
            };

            // Act
            IResult<Guid> result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<ConflictResult<Guid>>(result);
            Assert.False(result.IsSuccessful);
            Assert.NotEmpty(result.Errors);
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
                .Setup(m => m.GetByNameBrandAsync(It.Is<string>(p => p == existingProduct.Name), It.Is<string>(p => p == existingProduct.Brand), CancellationToken.None))
                .ReturnsAsync(value: null);

            _mocker.GetMock<IProductRepository>()
                .Setup(m => m.GetByIdAsync(It.Is<Guid>(p => p == existingProduct.Id), CancellationToken.None))
                .ReturnsAsync(existingProduct);

            var command = new UpdateProductCommand()
            {
                Id = existingProduct.Id,
                Name = "Name",
                Brand = "Brand",
                Description = "Description",
                Price = 100.12m,
                ProductCode = "ProductCode"
            };

            // Act
            IResult<Guid> result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<SuccessResult<Guid>>(result);
            Assert.True(result.IsSuccessful);
            Assert.Empty(result.Errors);

            Assert.Equal(existingProduct.Id, result.Data);
        }

        [Fact]
        public async Task UpdateProductCommand_VerifyCommandModelMapping()
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
                .Setup(m => m.GetByNameBrandAsync(It.Is<string>(p => p == existingProduct.Name), It.Is<string>(p => p == existingProduct.Brand), CancellationToken.None))
                .ReturnsAsync(value: null);

            _mocker.GetMock<IProductRepository>()
                .Setup(m => m.GetByIdAsync(It.Is<Guid>(p => p == existingProduct.Id), CancellationToken.None))
                .ReturnsAsync(existingProduct);

            Models.Product repositoryModel = null;
            _mocker.GetMock<IProductRepository>()
                .Setup(m => m.UpdateAsync(It.IsAny<Models.Product>(), It.IsAny<CancellationToken>()))
                .Callback((Models.Product product, CancellationToken cancellationToken) =>
                {
                    repositoryModel = product;
                });

            var command = new UpdateProductCommand()
            {
                Id = existingProduct.Id,
                Name = "Name",
                Brand = "Brand",
                Description = "Description",
                Price = 100.12m,
                ProductCode = "ProductCode"
            };

            // Act
            IResult<Guid> result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(command.Brand, repositoryModel.Brand);
            Assert.Equal(command.Description, repositoryModel.Description);
            Assert.Equal(command.Name, repositoryModel.Name);
            Assert.Equal(command.Price, repositoryModel.Price);
            Assert.Equal(command.ProductCode, repositoryModel.ProductCode);
        }
    }
}
