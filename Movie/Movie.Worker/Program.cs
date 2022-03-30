using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Persistance.Context;
using Movie.Worker.Extensions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Worker
{
    public class Program
    {
        static IConfiguration Configuration { get; set; }
        public static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();


            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddServices();

                    services.AddDbContext<MovieDBContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("MovieDBContextConnection")));
                })
               .UseSerilog();
    }
}
