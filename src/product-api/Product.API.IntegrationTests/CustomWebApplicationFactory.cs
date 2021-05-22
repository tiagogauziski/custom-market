using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Product.Infrastructure.Database.EntityFramework;

namespace Product.API.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's DbContext registration, if exists
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ProductContext>));

                if (descriptor is not null)
                    services.Remove(descriptor);

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddEntityFrameworkInMemoryDatabase();
                services.AddDbContext<ProductContext>((context) =>
                {
                    context.UseInMemoryDatabase("product")
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

                services.AddInfrastructureDatabaseEntityFramework();
            });
        }
    }
}
