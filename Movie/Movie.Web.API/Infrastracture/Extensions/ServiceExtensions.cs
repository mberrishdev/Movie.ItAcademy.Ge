using Microsoft.Extensions.DependencyInjection;
using Movie.Services.Abstractions;
using Movie.Web.Services.Implementations;
using Movie.Web.Services.Abstractions;
using Movie.Web.API.Infrastracture.Mappings;
using Movie.Services.Implementations;

namespace Movie.Web.API.Infrastracture.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.RegisterMaps();
            services.AddSingleton<Services.Abstractions.IJwtService, Services.Implementations.JwtService>();

            services.AddScoped<Services.Abstractions.IAccountService, Services.Implementations.AccountService>();
            services.AddScoped<IServerOptionService, ServerOptionService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddRepositories();
        }
    }
}
