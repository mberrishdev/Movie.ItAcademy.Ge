using Microsoft.AspNetCore.Http;
using Movie.Services.Abstractions;
using System.Threading.Tasks;

namespace Movie.Web.MVC.Infrastracture.Middlewares
{
    public class ServerOptionsLoaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServerOptionService _serverOptionService;

        public ServerOptionsLoaderMiddleware(RequestDelegate next, IServerOptionService serverOptionService)
        {
            _next = next;
            _serverOptionService = serverOptionService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _serverOptionService.LoadServerOptions();
            await _next(context);
        }
    }
}
