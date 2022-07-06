using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.BO.Web.MVC.Infrastracture.StartupConfiguration;
using Movie.Persistance.Context;
using Serilog;
using System;
using System.IO;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
                //.WriteTo.MSSqlServer(
                //connectionString: configuration.GetSection("ConnectionStrings:MovieDBContextConnection").Value,
                //tableName: configuration.GetSection("Serilog:TableName").Value,
                //appConfiguration: configuration,
                //autoCreateSqlTable: true,
                //columnOptionsSection: configuration.GetSection("Serilog:ColumnOptions"),
                //schemaName: configuration.GetSection("Serilog:SchemaName").Value)
                .CreateLogger();


try
{
    Log.Information("Starting host");
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<MovieDBContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDBContextConnection"));
    });

    Log.Information("Configuring web host");
    builder.ConfigureHost();

    Log.Information("Configuring services");
    builder.ConfigureService();

    var app = builder.Build();
    Log.Information("Configuring middleware");
    app.ConfigureMiddleware();

    Log.Information("Starting app");
    app.Run();
    Log.Information("Stopping host");
    //return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpecredly");
}
finally
{
    Log.CloseAndFlush();
}
