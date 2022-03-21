using Microsoft.Extensions.DependencyInjection;
using Movie.Worker.Services.HostedServices;

namespace Movie.Worker.Extensions
{

    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {

            services.AddHostedService<BookingCancellerService>();
            services.AddRepositories();
        }
    }

}
