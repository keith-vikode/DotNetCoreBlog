using DotNetCoreBlog.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DotNetCoreBlog.Tests
{
    public class WriteShould : BlogTestFixture
    {
        private const string TargetUrl = "/Home/Write";

        [Fact]
        public async Task Create_Blog_Post_If_Params_Valid()
        {
            // arrange
            var model = new WritePostModel
            {
                UrlSlug = "test-post",
                Title = "Test post",
                Summary = "Test post summary.",
                Body = "Test post body.",
                Tags = "test,tags"
            };
            
            // act
            await Client.PostAsync("/Home/Write", model.ToFormContent());

            // assert
            using (var context = OpenContext())
            {
                var newPost = await context.Posts.FirstOrDefaultAsync(post => post.UrlSlug == model.UrlSlug);
                Assert.NotNull(newPost);
            }
        }

        [Fact]
        public async Task Not_Create_Blog_Post_If_Params_Invalid()
        {
            // arrange
            var model = new WritePostModel
            {
                // missing all fields
            };

            // act
            await Client.PostAsync("/Home/Write", model.ToFormContent());

            // assert
            using (var context = OpenContext())
            {
                var posts = await context.Posts.ToListAsync();
                Assert.Equal(0, posts.Count);
            }
        }

        [Fact]
        public async Task Return_400_If_Params_Invalid()
        {
            // arrange
            var model = new WritePostModel
            {
                // missing all params
            };

            // act
            var response = await Client.PostAsync("/Home/Write", model.ToFormContent());

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
