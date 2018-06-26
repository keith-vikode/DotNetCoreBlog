using DotNetCoreBlog.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNetCoreBlog.Tests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<Startup>
    {
        private Func<BlogContext> _createContext;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (ApplicationDbContext) using an in-memory 
                // database for testing.
                services.AddDbContext<BlogContext>(options =>
                {
                    options.UseInMemoryDatabase("Blog");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                _createContext = () =>
                {
                    var sp = services.BuildServiceProvider();
                    return sp.GetRequiredService<BlogContext>();
                };
            });
        }

        public BlogContext CreateContext() => _createContext();
    }
}
