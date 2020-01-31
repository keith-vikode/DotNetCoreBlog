using DotNetCoreBlog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DotNetCoreBlog.Middleware
{
    public class ProcessingTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _settings;
        private readonly ILogger _logger;

        public ProcessingTimeMiddleware(RequestDelegate next, IOptions<AppSettings> settings, ILoggerFactory loggerFactory)
        {
            _next = next;
            _settings = settings.Value;
            _logger = loggerFactory.CreateLogger<ProcessingTimeMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            const string key = "ProcessingTime";

            // start a new timer
            var watch = new Stopwatch();
            watch.Start();

            // add the stopwatch to the context items for use elsewhere
            context.Items[key] = watch;

            // run the rest of the web pipeline
            await _next(context);

            // for debugging and diagnostics, we'd like to warn if some page loads take too long
            if (watch.Elapsed > _settings.MinPageLoadTime)
            {
                _logger.LogWarning($"{context.Request.Method} call to {context.Request.Path} took more than 1s ({watch.Elapsed})");
            }
        }
    }
}
