using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
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
    }
}
