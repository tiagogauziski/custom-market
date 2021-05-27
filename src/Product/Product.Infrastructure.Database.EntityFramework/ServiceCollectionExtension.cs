using Microsoft.Extensions.DependencyInjection;
using Product.Infrastructure.Database.EntityFramework.Repositories;

namespace Product.Infrastructure.Database.EntityFramework
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
        public static void AddInfrastructureDatabaseEntityFramework(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductHistoryRepository, ProductHistoryRepository>();
        }
    }
}
