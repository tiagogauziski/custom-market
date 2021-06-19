using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Stock.Application.Product.Service;
using Xunit;

namespace Stock.Application.UnitTests.Product.Service
{
    public class ProductServiceTests
    {
        [Fact]
        public void Ctor_WhenNullProductServiceSettings_ShouldReturnException()
        {
            // Arrange

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductService productService = new ProductService(Mock.Of<HttpClient>(), null);
            });
        }
    }
}
