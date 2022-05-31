using Microsoft.AspNetCore.Builder;
using Movie.Web.MVC.Infrastracture.Middlewares;

namespace Movie.Web.MVC.Infrastracture.Extensions
{
    public static class ServerOptionsLoaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseServerOptionsLoaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ServerOptionsLoaderMiddleware>();
        }
    }
}
