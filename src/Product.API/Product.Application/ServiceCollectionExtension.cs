using Microsoft.Extensions.DependencyInjection;
using Product.Application.Command;
using Product.Application.Event;
using Product.Application.Query;

namespace Product.Application
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
            services.AddApplicationCommandLayer();
            services.AddApplicationEventLayer();
            services.AddApplicationQueryLayer();
        }
    }
}
