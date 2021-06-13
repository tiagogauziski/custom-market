using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using FluentResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Stock.Application.Stock.Command;
using Xunit;

namespace Stock.API.IntegrationTests.Controllers
{
    public class StockControllerTests
       : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public StockControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = new CustomWebApplicationFactory<Startup>();

            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions());
        }

        [Fact]
        public async Task Increase_WhenInvalidProductId_ShouldReturnBadRequest()
        {
            // Arrange
            IncreaseStockCommand increaseStockCommand = GenerateIncreaseStockCommand();
            increaseStockCommand.ProductId = Guid.Empty;

            // Act
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/stock/{increaseStockCommand.ProductId}/increase",
               new StringContent(JsonConvert.SerializeObject(increaseStockCommand), Encoding.UTF8, "application/json"));
            string content = await response.Content.ReadAsStringAsync();
            Error[] responseViewModel = JsonConvert.DeserializeObject<Error[]>(content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEmpty(responseViewModel);
        }

        [Fact]
        public async Task Increase_WhenInvalidQuantity_ShouldReturnBadRequest()
        {
            // Arrange
            IncreaseStockCommand increaseStockCommand = GenerateIncreaseStockCommand();
            increaseStockCommand.Quantity = -1;

            // Act
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/stock/{increaseStockCommand.ProductId}/increase",
               new StringContent(JsonConvert.SerializeObject(increaseStockCommand), Encoding.UTF8, "application/json"));
            string content = await response.Content.ReadAsStringAsync();
            Error[] responseViewModel = JsonConvert.DeserializeObject<Error[]>(content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEmpty(responseViewModel);
        }

        [Fact]
        public async Task Increase_WhenValidCommand_ShouldReturnOk()
        {
            // Arrange
            IncreaseStockCommand increaseStockCommand = GenerateIncreaseStockCommand();

            // Act
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/stock/{increaseStockCommand.ProductId}/increase",
               new StringContent(JsonConvert.SerializeObject(increaseStockCommand), Encoding.UTF8, "application/json"));
            string content = await response.Content.ReadAsStringAsync();
            Guid responseViewModel = JsonConvert.DeserializeObject<Guid>(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(Guid.Empty, responseViewModel);
        }

        private IncreaseStockCommand GenerateIncreaseStockCommand()
        {
            Faker<IncreaseStockCommand> commandGenerator = new Faker<IncreaseStockCommand>()
               .RuleFor(command => command.ProductId, (faker, property) => faker.Random.Guid())
               .RuleFor(command => command.Quantity, (faker, property) => faker.Random.Int(1, 200));

            return commandGenerator.Generate();
        }
    }
}
