using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IServerOptionService, ServerOptionService>();
            services.AddScoped<IBookingService, BookingService>();
            //services.AddSingleton<IRoomService, RoomService>();
            //services.AddSingleton<ILogService, LogService>();
            //services.AddSingleton<IWebServices, WebServices>();
            //services.AddSingleton<IHttpRequestServices, HttpRequestServices>();
            //services.AddSingleton<IMessageSenderService, MessageSenderService>();
            //services.AddSingleton<IEmailService, EmailService>();

            services.AddRepositories();

            services.AddHostedService<BookingCancellerWorker>();
            //services.AddHostedService<EmailRemainderWorker>();
            //services.AddHostedService<LogsArchiverWorker>();
            //services.AddHostedService<WebDataRelodeWorker>();
            //services.AddHostedService<LogsArchiverWorker>();
            //services.AddHostedService<EmailRemainderWorker>();
        }
    }

}
