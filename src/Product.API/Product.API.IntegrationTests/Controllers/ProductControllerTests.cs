using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Product.Application.Command.Product.Commands;
using Product.Application.Query.Product.Responses;
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
            CreateProductCommand createCommand = GenerateCreateProductCommand();

            // Act
            HttpResponseMessage response = await _client.PostAsync("/api/v1/product",
               new StringContent(JsonConvert.SerializeObject(createCommand), Encoding.UTF8, "application/json"));
            string content = await response.Content.ReadAsStringAsync();
            Guid responseViewModel = JsonConvert.DeserializeObject<Guid>(content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotEqual(Guid.Empty, responseViewModel);
        }

        [Fact]
        public async Task Create_WhenInvalidProductName_ShouldReturnBadRequest()
        {
            // Arrange
            CreateProductCommand createCommand = GenerateCreateProductCommand();
            createCommand.Name = string.Empty;

            // Act
            HttpResponseMessage response = await _client.PostAsync("/api/v1/product",
               new StringContent(JsonConvert.SerializeObject(createCommand), Encoding.UTF8, "application/json"));
            string content = await response.Content.ReadAsStringAsync();
            string[] responseViewModel = JsonConvert.DeserializeObject<string[]>(content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEmpty(responseViewModel);
        }

        [Fact]
        public async Task Create_WhenDuplicatedProductNameBrand_ShouldReturnConflict()
        {
            // Arrange
            CreateProductCommand createCommand = GenerateCreateProductCommand();

            // Act
            HttpResponseMessage createResponse = await _client.PostAsync("/api/v1/product",
               new StringContent(JsonConvert.SerializeObject(createCommand), Encoding.UTF8, "application/json"));

            HttpResponseMessage duplicateResponse = await _client.PostAsync("/api/v1/product",
               new StringContent(JsonConvert.SerializeObject(createCommand), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
            Assert.Equal(HttpStatusCode.Conflict, duplicateResponse.StatusCode);
        }

        [Fact]
        public async Task Update_WhenValidProduct_ShouldReturnOk()
        {
            // Arrange
            CreateProductCommand createCommand = GenerateCreateProductCommand();

            // Act
            HttpResponseMessage createResponse = await _client.PostAsync("/api/v1/product",
               new StringContent(JsonConvert.SerializeObject(createCommand), Encoding.UTF8, "application/json"));
            Guid createGuid = JsonConvert.DeserializeObject<Guid>(await createResponse.Content.ReadAsStringAsync());

            Faker faker = new Faker();
            UpdateProductCommand updateCommand = new UpdateProductCommand()
            {
                Id = createGuid,
                Name = faker.Commerce.ProductName(),
                Brand = faker.Company.CompanyName(),
                Description = faker.Commerce.ProductDescription(),
                Price = faker.Finance.Amount(0, 1000)
            };

            HttpResponseMessage updateResponse = await _client.PutAsync($"/api/v1/product/{createGuid}",
               new StringContent(JsonConvert.SerializeObject(updateCommand), Encoding.UTF8, "application/json"));
            Guid updateGuid = JsonConvert.DeserializeObject<Guid>(await updateResponse.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
            Assert.Equal(createGuid, updateGuid);
        }

        [Fact]
        public async Task Update_WhenInvalidGuid_ShouldReturnNotFound()
        {
            // Arrange

            // Act
            Faker faker = new Faker();
            UpdateProductCommand updateCommand = new UpdateProductCommand()
            {
                Id = Guid.NewGuid(),
                Name = faker.Commerce.ProductName(),
                Brand = faker.Company.CompanyName(),
                Description = faker.Commerce.ProductDescription(),
                Price = faker.Finance.Amount(0, 1000)
            };

            HttpResponseMessage updateResponse = await _client.PutAsync($"/api/v1/product/{updateCommand.Id}",
               new StringContent(JsonConvert.SerializeObject(updateCommand), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
        }

        [Fact]
        public async Task GetById_WhenValidProduct_ShouldReturnOk()
        {
            // Arrange
            CreateProductCommand createCommand = GenerateCreateProductCommand();

            // Act
            HttpResponseMessage createResponse = await _client.PostAsync("/api/v1/product",
               new StringContent(JsonConvert.SerializeObject(createCommand), Encoding.UTF8, "application/json"));
            Guid createGuid = JsonConvert.DeserializeObject<Guid>(await createResponse.Content.ReadAsStringAsync());

            HttpResponseMessage getByIdResponse = await _client.GetAsync($"/api/v1/product/{createGuid}");
            ProductResponseModel getByIdResponseModel =
                JsonConvert.DeserializeObject<ProductResponseModel>(await getByIdResponse.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, getByIdResponse.StatusCode);
            Assert.Equal(createCommand.Name, getByIdResponseModel.Name);
            Assert.Equal(createCommand.Brand, getByIdResponseModel.Brand);
            Assert.Equal(createCommand.Description, getByIdResponseModel.Description);
            Assert.Equal(createCommand.Price, getByIdResponseModel.Price);
            Assert.Equal(createCommand.ProductCode, getByIdResponseModel.ProductCode);
        }

        [Fact]
        public async Task GetById_WhenInvalidProductId_ShouldReturnNotFound()
        {
            // Arrange
            Guid invalidProduct = Guid.NewGuid();

            // Act
            HttpResponseMessage getByIdResponse = await _client.GetAsync($"/api/v1/product/{invalidProduct}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, getByIdResponse.StatusCode);
        }

        [Fact]
        public async Task Delete_WhenValidProduct_ShouldReturnNoContent()
        {
            // Arrange
            CreateProductCommand createCommand = GenerateCreateProductCommand();

            // Act
            HttpResponseMessage createResponse = await _client.PostAsync("/api/v1/product",
               new StringContent(JsonConvert.SerializeObject(createCommand), Encoding.UTF8, "application/json"));
            Guid createGuid = JsonConvert.DeserializeObject<Guid>(await createResponse.Content.ReadAsStringAsync());

            HttpResponseMessage updateResponse = await _client.DeleteAsync($"/api/v1/product/{createGuid}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);
        }

        [Fact]
        public async Task Delete_WhenInvalidProduct_ShouldReturnNotFound()
        {
            // Arrange
            Guid invalidProduct = Guid.NewGuid();

            // Act
            HttpResponseMessage updateResponse = await _client.DeleteAsync($"/api/v1/product/{invalidProduct}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
        }

        private static CreateProductCommand GenerateCreateProductCommand()
        {
            Faker<CreateProductCommand> testProduct = new Faker<CreateProductCommand>()
                .RuleFor(product => product.Name, (faker, property) => faker.Commerce.ProductName())
                .RuleFor(product => product.Brand, (faker, property) => faker.Company.CompanyName())
                .RuleFor(product => product.Description, (faker, property) => faker.Commerce.ProductDescription())
                .RuleFor(product => product.Price, (faker, property) => faker.Finance.Amount(0, 1000))
                .RuleFor(product => product.ProductCode, (faker, property) => $"{property.Name}-{property.Brand}");

            return testProduct.Generate();
        }
    }
}
