using Microsoft.Extensions.DependencyInjection;
using Movie.Services.Abstractions;
using Movie.Services.Implementations;
using Movie.Worker.Services.Abstractions;
using Movie.Worker.Services.BackgroudWorkers;
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
            services.AddSingleton<IMessageSenderService, MessageSenderService>();
            services.AddSingleton<IEmailService, EmailService>();

            services.AddRepositories();

            services.AddHostedService<BookingCancellerWorker>();
            Thread.Sleep(3000);
            services.AddHostedService<EmailRemainderWorker>();
            Thread.Sleep(3000);
            services.AddHostedService<LogsArchiverWorker>();
            Thread.Sleep(3000);
            services.AddHostedService<WebDataRelodeWorker>();
            Thread.Sleep(3000);
            services.AddHostedService<LogsArchiverWorker>();
            Thread.Sleep(3000);
            services.AddHostedService<EmailRemainderWorker>();
        }
    }

}
