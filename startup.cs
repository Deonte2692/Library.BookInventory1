using Library.BookInventory.Controllers;
using Library.BookInventory.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.BookInventory
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Configure services (DI container)
        public void ConfigureServices(IServiceCollection services)
        {
            // Register BookController and BookRepository for dependency injection
            services.AddScoped<BookController>();
            services.AddScoped<BookRepository>();

            // Configure Entity Framework with MySQL
            services.AddDbContext<LibraryContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))
                )
            );

            // Add MVC services for web pages
            services.AddControllersWithViews();  // This enables MVC-style views

            // If you need Razor Pages (optional)
            // services.AddRazorPages();
        }

        // Configure HTTP request pipeline (middleware)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Enable static files (CSS, JS, images)
            app.UseStaticFiles();

            // Use routing for MVC
            app.UseRouting();

            // Map controller routes
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Book}/{action=Index}/{id?}");  // Default route to Book controller's Index action
            });
        }
    }
}
