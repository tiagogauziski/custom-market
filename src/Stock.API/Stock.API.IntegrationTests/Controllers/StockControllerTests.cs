using System;
using System.Linq;
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
using Stock.Application.Stock.Errors;
using Xunit;

namespace Stock.API.IntegrationTests.Controllers
{
    public class StockControllerTests
       : IClassFixture<StockApiWebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        private readonly Guid _productId = new Guid("8ba16d3d-608e-4cb1-834e-d1e6c33c6cff");
        private readonly Guid _outOfStockProduct = Guid.NewGuid();
        private readonly Guid _newProduct = Guid.NewGuid();

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
                    mockHttp.When(HttpMethod.Get, $"http://localhost/api/v1/product/{_productId}")
                            .Respond(HttpStatusCode.OK, "application/json", JsonConvert.SerializeObject(new ProductResponse()
                            {
                                Id = _productId,
                                Name = "TestName",
                                Brand = "TestBrand",
                                ProductCode = "TestProductCode"
                            })); // Respond with JSON

                    mockHttp.When(HttpMethod.Get, $"http://localhost/api/v1/product/{_outOfStockProduct}")
                            .Respond(HttpStatusCode.OK, "application/json", JsonConvert.SerializeObject(new ProductResponse()
                            {
                                Id = _outOfStockProduct,
                                Name = "OutOfStockName",
                                Brand = "OutOfStockBrand",
                                ProductCode = "OutOfStockProductCode"
                            })); // Respond with JSON

                    mockHttp.When(HttpMethod.Get, $"http://localhost/api/v1/product/{_newProduct}")
                            .Respond(HttpStatusCode.OK, "application/json", JsonConvert.SerializeObject(new ProductResponse()
                            {
                                Id = _newProduct,
                                Name = "NewName",
                                Brand = "NewBrand",
                                ProductCode = "NewProductCode"
                            })); // Respond with JSON
                    services
                        .AddHttpClient<IProductService, ProductService>()
                        .ConfigurePrimaryHttpMessageHandler(configureHandler => mockHttp);
                });
            });

            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions());
        }

        [Fact]
        public async Task IncreaseAsync_WhenInvalidProductId_ShouldReturnBadRequest()
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
        public async Task IncreaseAsync_WhenInvalidQuantity_ShouldReturnBadRequest()
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
        public async Task IncreaseAsync_WhenValidCommand_ShouldReturnOk()
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

        [Fact]
        public async Task DecreaseAsync_WhenInvalidProductId_ShouldReturnBadRequest()
        {
            // Arrange
            DecreaseStockCommand decreaseStockCommand = GenerateDecreaseStockCommand();
            decreaseStockCommand.ProductId = Guid.Empty;

            // Act
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/stock/{decreaseStockCommand.ProductId}/decrease",
               new StringContent(JsonConvert.SerializeObject(decreaseStockCommand), Encoding.UTF8, "application/json"));
            string content = await response.Content.ReadAsStringAsync();
            string[] responseViewModel = JsonConvert.DeserializeObject<string[]>(content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEmpty(responseViewModel);
        }

        [Fact]
        public async Task DecreaseAsync_WhenInvalidQuantity_ShouldReturnBadRequest()
        {
            // Arrange
            DecreaseStockCommand decreaseStockCommand = GenerateDecreaseStockCommand();
            decreaseStockCommand.Quantity = -1;

            // Act
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/stock/{decreaseStockCommand.ProductId}/decrease",
               new StringContent(JsonConvert.SerializeObject(decreaseStockCommand), Encoding.UTF8, "application/json"));
            string content = await response.Content.ReadAsStringAsync();
            string[] responseViewModel = JsonConvert.DeserializeObject<string[]>(content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEmpty(responseViewModel);
        }

        [Fact]
        public async Task DecreaseAsync_WhenProductIsOutOfStock_ShouldReturnBadRequest()
        {
            // Arrange
            DecreaseStockCommand decreaseStockCommand = GenerateDecreaseStockCommand();
            decreaseStockCommand.ProductId = _outOfStockProduct;

            // Act
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/stock/{decreaseStockCommand.ProductId}/decrease",
               new StringContent(JsonConvert.SerializeObject(decreaseStockCommand), Encoding.UTF8, "application/json"));

            string content = await response.Content.ReadAsStringAsync();
            string[] responseViewModel = JsonConvert.DeserializeObject<string[]>(content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEmpty(responseViewModel);
            Assert.Equal(new ProductOutOfStockError().Message, responseViewModel.First());
        }

        [Fact]
        public async Task DecreaseAsync_WhenDecreaseQuantityIsHigher_ShouldReturnBadRequest()
        {
            // Arrange
            IncreaseStockCommand increaseStockCommand =new IncreaseStockCommand()
            {
                ProductId = _newProduct,
                Quantity = 1
            };

            // Act
            HttpResponseMessage increaseResponse = await _client.PutAsync($"/api/v1/stock/{increaseStockCommand.ProductId}/increase",
               new StringContent(JsonConvert.SerializeObject(increaseStockCommand), Encoding.UTF8, "application/json"));

            increaseResponse.EnsureSuccessStatusCode();

            DecreaseStockCommand decreaseStockCommand = new DecreaseStockCommand()
            {
                ProductId = _newProduct,
                Quantity = 2
            };

            HttpResponseMessage decreaseResponse = await _client.PutAsync($"/api/v1/stock/{decreaseStockCommand.ProductId}/decrease",
               new StringContent(JsonConvert.SerializeObject(decreaseStockCommand), Encoding.UTF8, "application/json"));

            string content = await decreaseResponse.Content.ReadAsStringAsync();
            string[] responseViewModel = JsonConvert.DeserializeObject<string[]>(content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, decreaseResponse.StatusCode);
            Assert.NotEmpty(responseViewModel);
            Assert.Equal(new ProductOutOfStockError().Message, responseViewModel.First());
        }

        [Fact]
        public async Task DecreaseAsync_WhenValidCommand_ShouldReturnOk()
        {
            // Arrange
            IncreaseStockCommand increaseStockCommand = GenerateIncreaseStockCommand();
            increaseStockCommand.Quantity = 1;

            // Act
            HttpResponseMessage increaseResponse = await _client.PutAsync($"/api/v1/stock/{increaseStockCommand.ProductId}/increase",
               new StringContent(JsonConvert.SerializeObject(increaseStockCommand), Encoding.UTF8, "application/json"));

            increaseResponse.EnsureSuccessStatusCode();

            DecreaseStockCommand decreaseStockCommand = GenerateDecreaseStockCommand();
            decreaseStockCommand.Quantity = 1;

            HttpResponseMessage decreaseResponse = await _client.PutAsync($"/api/v1/stock/{decreaseStockCommand.ProductId}/decrease",
               new StringContent(JsonConvert.SerializeObject(decreaseStockCommand), Encoding.UTF8, "application/json"));

            string content = await decreaseResponse.Content.ReadAsStringAsync();
            Guid responseViewModel = JsonConvert.DeserializeObject<Guid>(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, increaseResponse.StatusCode);
            Assert.NotEqual(Guid.Empty, responseViewModel);
        }

        private IncreaseStockCommand GenerateIncreaseStockCommand()
        {
            Faker<IncreaseStockCommand> commandGenerator = new Faker<IncreaseStockCommand>()
               .RuleFor(command => command.ProductId, (faker, property) => _productId)
               .RuleFor(command => command.Quantity, (faker, property) => faker.Random.Int(1, 200));

            return commandGenerator.Generate();
        }

        private DecreaseStockCommand GenerateDecreaseStockCommand()
        {
            Faker<DecreaseStockCommand> commandGenerator = new Faker<DecreaseStockCommand>()
               .RuleFor(command => command.ProductId, (faker, property) => _productId)
               .RuleFor(command => command.Quantity, (faker, property) => faker.Random.Int(1, 200));

            return commandGenerator.Generate();
        }
    }
}
