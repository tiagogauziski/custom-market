using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Product.Application.Query
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
        public static void AddApplicationQueryLayer(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtension));
        }
    }
}
