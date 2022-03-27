using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;

namespace Movie.Web.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                connectionString: configuration.GetSection("ConnectionStrings:MovieDBContextConnection").Value,
                tableName: configuration.GetSection("Serilog:TableName").Value,
                appConfiguration: configuration,
                autoCreateSqlTable: true,
                columnOptionsSection: configuration.GetSection("Serilog:ColumnOptions"),
                schemaName: configuration.GetSection("Serilog:SchemaName").Value)
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
