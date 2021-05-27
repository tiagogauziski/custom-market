using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using Product.Application.Query.Product.Handlers;
using Product.Application.Query.Product.Queries;
using Product.Infrastructure.Database;
using Xunit;

namespace Product.Application.Query.UnitTests
{
    public class ProductQueryHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly ProductQueryHandler _productQueryHandler;

        public ProductQueryHandlerTests()
        {
            _mocker = new AutoMocker();

            _productQueryHandler = _mocker.CreateInstance<ProductQueryHandler>();
        }

        [Fact]
        public void ProductQueryHandler_Ctor_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var handler = new ProductQueryHandler(null);
            });
        }

        [Fact]
        public void GetProductByIdQuery_InvalidQueryParameter_ThrowException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _productQueryHandler.Handle(null, CancellationToken.None).ConfigureAwait(false);
            });
        }

        [Fact]
        public async Task GetProductByIdQuery_MatchId_Success()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            var productModel = new Models.Product()
            {
                Id = productId,
                Brand = "Brand",
                Description = "Description",
                Name = "Name",
                Price = 123.45,
                ProductCode = "ProductCode"
            };

            _mocker.GetMock<IProductRepository>()
                .Setup(m => m.GetByIdAsync(It.Is<Guid>(id => id == productId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(productModel)
                .Verifiable();

            // Act
            var productResponse =
                await _productQueryHandler.Handle(
                    new GetProductByIdQuery(productId),
                    CancellationToken.None).ConfigureAwait(false);

            // Assert
            _mocker.VerifyAll();
            Assert.NotNull(productResponse);
            Assert.Equal(productModel.Id, productResponse.Id);
            Assert.Equal(productModel.Name, productResponse.Name);
            Assert.Equal(productModel.Brand, productResponse.Brand);
            Assert.Equal(productModel.Description, productResponse.Description);
            Assert.Equal(productModel.Price, productResponse.Price);
            Assert.Equal(productModel.ProductCode, productResponse.ProductCode);
        }

        [Fact]
        public async Task GetProductByIdQuery_IdDoesNotMatch_Success()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            var productModel = new Models.Product();

            _mocker.GetMock<IProductRepository>()
                .Setup(m => m.GetByIdAsync(It.Is<Guid>((id) => id == productId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(productModel);

            // Act
            var productResponse =
                await _productQueryHandler.Handle(
                    new GetProductByIdQuery(Guid.NewGuid()),
                    CancellationToken.None).ConfigureAwait(false);

            // Assert
            Assert.Null(productResponse);
        }
    }
}
