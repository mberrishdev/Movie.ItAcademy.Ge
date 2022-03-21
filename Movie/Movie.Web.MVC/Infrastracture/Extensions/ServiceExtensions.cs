using Microsoft.Extensions.DependencyInjection;
using Movie.Services.Abstractions;
using Movie.Services.Implementations;
using Movie.Web.MVC.Infrastracture.Mappings;

namespace Movie.Web.MVC.Infrastracture.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.RegisterMaps();
            services.AddScoped<IAccountService, AccountService>();

            services.AddRepositories();
        }
    }
}
