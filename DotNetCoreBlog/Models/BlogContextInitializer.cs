using Microsoft.EntityFrameworkCore;

namespace DotNetCoreBlog.Models
{
    public static class BlogContextInitializer
    {
        // Note: you could add seed data here if you wanted to
        public static void Initialize(BlogContext context)
        {
            context.Database.Migrate();
        }
    }
}
