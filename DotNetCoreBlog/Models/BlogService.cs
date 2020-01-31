using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreBlog.Models
{
    public class BlogService
    {
        private readonly ILogger _logger;
        private readonly BlogContext _context;

        public BlogService(BlogContext context, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BlogService>();
            _context = context;
        }

        public async Task<BlogPost> GetPostByIdAsync(int id)
        {
            _logger.LogInformation($"Loading post {id}");
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                _logger.LogWarning($"Could not find post {id}");
            }

            return post;
        }

        public async Task<BlogPost> GetPostByUrlAsync(string slug)
        {
            _logger.LogInformation($"Loading post /{slug}");
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.UrlSlug == slug);
            if (post == null)
            {
                _logger.LogWarning($"Could not find post /{slug}");
            }

            return post;
        }

        public async Task<IEnumerable<BlogSummary>> GetRecentPostsAsync(int limit)
        {
            _logger.LogInformation($"Loading most recent {limit} posts");

            var posts = await _context.Posts
                .OrderByDescending(post => post.PubDate)
                .Take(limit)
                .Select(p => new { p.UrlSlug, p.Title, p.Summary, p.Tags })
                .ToListAsync();

            return posts.Select(p => new BlogSummary { UrlSlug = p.UrlSlug, Title = p.Title, Summary = p.Summary, Tags = p.Tags });
        }

        public async Task<BlogPost> WritePostAsync(string slug, string title, string summary, string body, string tags = null)
        {
            if (await DuplicateSlugExistsAsync(slug))
            {
                throw new ArgumentException($"The URL slug {slug} is already in use", nameof(slug));
            }

            var post = new BlogPost { UrlSlug = slug, Title = title, Summary = summary, Body = body, Tags = tags };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Created new post at /{slug} with ID {post.Id}");

            return post;
        }

        public Task<bool> DuplicateSlugExistsAsync(string slug)
        {
            _logger.LogInformation($"Checking uniqueness of URL {slug}");

            return _context.Posts.AnyAsync(post => post.UrlSlug == slug);
        }
    }
}
