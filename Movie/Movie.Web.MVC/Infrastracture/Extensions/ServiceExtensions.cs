using Microsoft.Extensions.DependencyInjection;
using Movie.Services.Abstractions;
using Movie.Web.Services.Implementations;
using Movie.Web.MVC.Infrastracture.Mappings;
using Movie.Web.Services.Abstractions;

namespace Movie.Web.MVC.Infrastracture.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.RegisterMaps();
            services.AddScoped<IAccountService, AccountService>();
            services.AddSingleton<IRoomService, RoomService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddRepositories();
        }
    }
}
