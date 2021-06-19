using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Stock.API
{
    /// <summary>
    /// Entry start for the web application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entrypoint method.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        /// <summary>
        /// Creates a web host.
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <returns>A <see cref="IHostBuilder"/> instance.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
