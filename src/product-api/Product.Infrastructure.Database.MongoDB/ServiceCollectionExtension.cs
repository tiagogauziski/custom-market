using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Product.Infrastructure.Database.MongoDB.Repositories;
using Product.Infrastructure.Database.MongoDB.Settings;

namespace Product.Infrastructure.Database.MongoDB
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Add MongoDB database implementation dependencies into <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Dependency injection container.</param>
        public static void AddDatabaseInfrastructure(this IServiceCollection services)
        {
            DatabaseMapping.RegisterMapping();

            services.AddSingleton((serviceProvider) =>
            {
                var settings = serviceProvider.GetRequiredService<IProductDatabaseSettings>();

                return new MongoClient(settings.ConnectionString);
            });

            services.AddSingleton<IProductRepository, ProductRepository>();
        }
    }
}
