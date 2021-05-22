using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Product.Application.Command.Product.Commands;
using Xunit;

namespace Product.API.IntegrationTests.Controllers
{
    public class ProductControllerTests
        : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public ProductControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = new CustomWebApplicationFactory<Startup>();

            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions());
        }

        [Fact]
        public async Task Create_WhenValidProduct_ShouldReturnCreated()
        {
            // Arrange
            CreateProductCommand command = new CreateProductCommand()
            {
                Name = "Name",
                Brand = "Brand"
            };

            // Act
            var response = await _client.PostAsync("/api/v1/product",
               new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            var responseViewModel = JsonConvert.DeserializeObject<Guid>(content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotEqual(Guid.Empty, responseViewModel);
        }
    }
}
