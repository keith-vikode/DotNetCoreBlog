using DotNetCoreBlog.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetCoreBlog
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            // set up entity framework
            ConfigureDb(services);

            // set up application services
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.AddTransient<BlogService>();

            // set up framework services
            services.AddMvc();
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // plug in custom middleware - it's important that we do this first to get accurate results
            app.UseMiddleware<Middleware.ProcessingTimeMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        protected virtual void ConfigureDb(IServiceCollection services)
        {
            services.AddDbContext<BlogContext>(options =>
            {
                var sqlConnection = Configuration.GetConnectionString("Blog");
                options.UseSqlServer(sqlConnection, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
            });
        }
    }
}
