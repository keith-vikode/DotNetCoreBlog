using DotNetCoreBlog.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;
using System.Net.Http;

namespace DotNetCoreBlog.Tests
{
    public class BlogTestFixture : IDisposable
    {
        public BlogTestFixture()
        {
            // ugly hack to get the content root for the website project
            var integrationTestsPath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationPath = Path.GetFullPath(Path.Combine(integrationTestsPath, "../../../../DotNetCoreBlog"));

            var builder = new WebHostBuilder()
                .UseStartup<TestStartup>()
                .UseContentRoot(applicationPath);

            Server = new TestServer(builder);
            Client = Server.CreateClient();
        }

        public TestServer Server { get; }

        public HttpClient Client { get; }

        public BlogContext OpenContext() => TestStartup.CreateContext();

        public void Dispose()
        {
            // ensure we "clean" the database after each test
            OpenContext().Database.EnsureDeleted();
            Server.Dispose();
        }
    }
}
