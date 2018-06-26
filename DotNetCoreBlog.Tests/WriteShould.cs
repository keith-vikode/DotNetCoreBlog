using DotNetCoreBlog.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DotNetCoreBlog.Tests
{
    public class WriteShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public WriteShould(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Create_Blog_Post_If_Params_Valid()
        {
            // arrange
            var client = _factory.CreateClient();

            var model = new WritePostModel
            {
                UrlSlug = "test-post",
                Title = "Test post",
                Summary = "Test post summary.",
                Body = "Test post body.",
                Tags = "test,tags"
            };
            
            // act
            await client.PostAsync("/Home/Write", model.ToFormContent());

            // assert
            using (var context = _factory.CreateContext())
            {
                var newPost = await context.Posts.FirstOrDefaultAsync(post => post.UrlSlug == model.UrlSlug);
                Assert.NotNull(newPost);
            }
        }

        [Fact]
        public async Task Not_Create_Blog_Post_If_Params_Invalid()
        {
            // arrange
            var client = _factory.CreateClient();
            var model = new WritePostModel
            {
                // missing all fields
            };

            // act
            await client.PostAsync("/Home/Write", model.ToFormContent());

            // assert
            using (var context = _factory.CreateContext())
            {
                var posts = await context.Posts.ToListAsync();
                Assert.Empty(posts);
            }
        }

        [Fact]
        public async Task Return_400_If_Params_Invalid()
        {
            // arrange
            var client = _factory.CreateClient();
            var model = new WritePostModel
            {
                // missing all params
            };

            // act
            var response = await client.PostAsync("/Home/Write", model.ToFormContent());

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
