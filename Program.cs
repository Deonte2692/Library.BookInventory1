using Library.BookInventory.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Library.BookInventory.Models;
using Library.BookInventory.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace Library.BookInventory
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Create a web host builder
            var host = CreateHostBuilder(args).Build();
            host.Run();  // Run the web application
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Load appsettings.json
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();  // Use the Startup class for configuring services and middleware
                });

    }
}
