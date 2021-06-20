using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Stock.Application.Product.Contracts.V1;
using Stock.Application.Product.Service;
using Stock.Application.Stock.Commands;
using Xunit;

namespace Stock.API.IntegrationTests.Controllers
{
    public class StockControllerTests
       : IClassFixture<StockApiWebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public StockControllerTests(StockApiWebApplicationFactory<Startup> factory)
        {
            _factory = new StockApiWebApplicationFactory<Startup>();

            _factory = _factory.WithWebHostBuilder(configuration =>
            {
                configuration.ConfigureTestServices(services =>
                {
                    // TODO: use a better way of creating a fake http client to mock product api request/response.
                    var mockHttp = new MockHttpMessageHandler(BackendDefinitionBehavior.NoExpectations);

                    // Setup a respond for the user api (including a wildcard in the URL)
                    mockHttp.When(HttpMethod.Get, "http://localhost/api/v1/product/*")
                            .Respond(HttpStatusCode.OK, "application/json", JsonConvert.SerializeObject(new ProductResponse())); // Respond with JSON
                    services
                        .AddHttpClient<IProductService, ProductService>()
                        .ConfigurePrimaryHttpMessageHandler(configureHandler => mockHttp);
                });
            });

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
            string[] responseViewModel = JsonConvert.DeserializeObject<string[]>(content);

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
            string[] responseViewModel = JsonConvert.DeserializeObject<string[]>(content);

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

            response.EnsureSuccessStatusCode();

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
