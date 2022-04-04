using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Persistance.Context;
using Movie.Worker.Extensions;
using Serilog;
using System;

namespace Movie.Worker
{
    public class Program
    {
        static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("bin/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

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
