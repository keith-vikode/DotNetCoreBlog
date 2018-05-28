using System;
using DotNetCoreBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreBlog.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration config)
            : base(config)
        {
        }

        protected override void ConfigureDb(IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                return CreateContext();
            });
        }

        public static BlogContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase("Blog")
                .Options;
            return new BlogContext(options);
        }
    }
}
