﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RichardSzalay.MockHttp;
using Stock.Application.Product.Service;
using System.IO;
using System.Net.Http;

namespace Stock.API.IntegrationTests
{
    public class StockApiWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {

            string fullPath = System.Reflection.Assembly.GetAssembly(typeof(StockApiWebApplicationFactory<Startup>)).Location;
            string directory = Path.GetDirectoryName(fullPath);

            IHostBuilder builder = Host
                .CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder
                        .UseStartup<TStartup>()
                        .ConfigureTestServices(services =>
                        {
                        })
                        .UseTestServer();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(directory);
                }); ;
            return builder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
            });
        }
    }
}
