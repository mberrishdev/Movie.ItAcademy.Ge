using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Movie.BO.Web.MVC.Infrastracture.StartupConfiguration
{
    public static class HostConfiguration
    {
        public static WebApplicationBuilder ConfigureHost(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("appsetting.json", optional: true, reloadOnChange: true);

            builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext());

            return builder;
        }
    }
}
