using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Product.Application
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Add application dependency injections into <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Dependency injection container.</param>
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtension));
        }
    }
}
