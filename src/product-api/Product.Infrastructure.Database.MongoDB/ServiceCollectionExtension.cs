using Microsoft.Extensions.DependencyInjection;
using Product.Infrastructure.Database;
using Product.Infrastructure.Database.MongoDB.Repositories;

namespace Product.Application.Command
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
            services.AddSingleton<IProductRepository, ProductRepository>();
        }
    }
}
