using Microsoft.Extensions.DependencyInjection;
using Movie.Services.Abstractions;
using Movie.Services.Implementations;
using Movie.Worker.Services.Abstractions;
using Movie.Worker.Services.HostedServices;
using Movie.Worker.Services.Implementations;

namespace Movie.Worker.Extensions
{

    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IServerOptionService, ServerOptionService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IWebServices, WebServices>();
            services.AddScoped<IHttpRequestServices, HttpRequestServices>();

            services.AddHostedService<BookingCancellerService>();
            services.AddHostedService<RoomArchiverService>();
            services.AddHostedService<RoomCheckerService>();
            services.AddHostedService<WebDataRelodeService>();

            services.AddRepositories();
        }
    }

}
