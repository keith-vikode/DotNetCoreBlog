using DotNetCoreBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DotNetCoreBlog.Controllers
{
    public class HomeController : Controller
    {
        private BlogService _service;

        public HomeController(BlogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index([FromServices] IOptions<AppSettings> settings)
        {
            // purpose-built back-door to demonstrate the middleware
            if (Request.Query.Count > 0)
            {
                await Task.Delay(1000);
            }

            return View(await _service.GetRecentPostsAsync(settings.Value.MaxPostsOnHomepage));
        }

        public IActionResult About() => View();

        [HttpGet]
        public async Task<IActionResult> Blog(string id)
        {
            var post = await _service.GetPostByUrlAsync(id);
            if (post != null)
            {
                return View(post);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Write() => View(new WritePostModel());

        [HttpPost]
        public async Task<IActionResult> Write(WritePostModel model)
        {
            if (ModelState.IsValid)
            {
                var post = await _service.WritePostAsync(model.UrlSlug, model.Title, model.Summary, model.Body, model.Tags);
                return RedirectToAction(nameof(Blog), new { Id = post.UrlSlug });
            }

            Response.StatusCode = 400;
            return View(model);
        }

        public IActionResult Error(string id = null) => View(id ?? "500");
    }
}
