using DotNetCoreBlog.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DotNetCoreBlog.Tests
{
    public class WriteShould : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public WriteShould(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Create_Blog_Post_If_Params_Valid()
        {
            // arrange
            HttpClient client = CreateClient();

            WritePostModel model = new WritePostModel
            {
                UrlSlug = "test-post",
                Title = "Test post",
                Summary = "Test post summary.",
                Body = "Test post body.",
                Tags = "test,tags"
            };

            // act
            await client.PostAsync("/Home/Write", model.ToFormContent());
            var result = await client.GetAsync($"/Home/Blog/{model.UrlSlug}");

            // assert
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Not_Found_For_Invalid_Blog_Entry()
        {
            // arrange
            HttpClient client = CreateClient();

            // act
            var result = await client.GetAsync("/Home/Blog/not-a-real-post");

            // assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Return_400_If_Params_Invalid()
        {
            // arrange
            HttpClient client = CreateClient();
            WritePostModel model = new WritePostModel
            {
                // missing all params
            };

            // act
            HttpResponseMessage response = await client.PostAsync("/Home/Write", model.ToFormContent());

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private HttpClient CreateClient()
        {
            return _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddEntityFrameworkInMemoryDatabase();
                    services.AddDbContext<BlogContext>(options =>
                    {
                        options.UseInMemoryDatabase("Blog");
                    });
                });
            }).CreateClient();
        }
    }
}
