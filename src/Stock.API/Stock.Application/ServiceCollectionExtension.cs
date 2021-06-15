using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Product.Service;

namespace Stock.Application
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Add application dependencies into <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Dependency injection container.</param>
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtension));
            services.AddHttpClient<IProductService, ProductService>();
        }
    }
}
