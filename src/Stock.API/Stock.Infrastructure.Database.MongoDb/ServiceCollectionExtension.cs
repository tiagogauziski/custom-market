using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Stock.Infrastructure.Database.Abstractions;
using Stock.Infrastructure.Database.MongoDb.Repositories;
using Stock.Infrastructure.Database.MongoDb.Settings;

namespace Stock.Infrastructure.Database.MongoDb
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
        public static void AddInfrastructureDatabaseMongoDb(this IServiceCollection services)
        {
            DatabaseMapping.RegisterMapping();

            services.AddSingleton((serviceProvider) =>
            {
                IStockDatabaseSettings settings = serviceProvider.GetRequiredService<IStockDatabaseSettings>();

                return new MongoClient(settings.ConnectionString);
            });

            services.AddSingleton<IStockRespository, StockRepository>();
            services.AddSingleton<IStockHistoryRepository, StockHistoryRepository>();
        }
    }
}
