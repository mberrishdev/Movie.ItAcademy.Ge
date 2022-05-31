using Microsoft.AspNetCore.Builder;
using Movie.BO.Web.MVC.Infrastracture.Middlewares;

namespace Movie.BO.Web.MVC.Infrastracture.Extensions
{
    public static class ServerOptionsLoaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseServerOptionsLoaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ServerOptionsLoaderMiddleware>();
        }
    }
}
