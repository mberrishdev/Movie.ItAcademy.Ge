using Microsoft.Extensions.DependencyInjection;
using Movie.Services.Abstractions;
using Movie.Services.Implementations;
using Movie.Worker.Services.Abstractions;
using Movie.Worker.Services.HostedServices;
using Movie.Worker.Services.Implementations;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.Worker.Extensions
{

    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IServerOptionService, ServerOptionService>();
            services.AddSingleton<IBookingService, BookingService>();
            services.AddSingleton<IRoomService, RoomService>();
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IWebServices, WebServices>();
            services.AddSingleton<IHttpRequestServices, HttpRequestServices>();
            services.AddSingleton<IMessageSenderService, Services.Implementations.MessageSenderService>();
            services.AddSingleton<IEmailService, Services.Implementations.EmailService>();

            services.AddHostedService<BookingCancellerService>();
            Thread.Sleep(10000);
            services.AddHostedService<RoomArchiverService>();
            services.AddHostedService<RoomCheckerService>();
            services.AddHostedService<WebDataRelodeService>();
            services.AddHostedService<LogsArchiverService>();
            services.AddHostedService<Services.HostedServices.MessageSenderService>();
            services.AddHostedService<Services.HostedServices.EmailService>();

            services.AddRepositories();
        }
    }

}
