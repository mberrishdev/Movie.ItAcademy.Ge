using Microsoft.Extensions.DependencyInjection;
using Movie.Services.Abstractions;
using Movie.Web.Services.Implementations;
using Movie.Web.MVC.Infrastracture.Mappings;
using Movie.Web.Services.Abstractions;
using Movie.Services.Implementations;

namespace Movie.Web.MVC.Infrastracture.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.RegisterMaps();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IServerOptionService, ServerOptionService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddRepositories();
        }
    }
}
