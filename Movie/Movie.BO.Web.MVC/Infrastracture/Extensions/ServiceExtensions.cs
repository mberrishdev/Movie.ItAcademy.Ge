using Microsoft.Extensions.DependencyInjection;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Implementations;
using Movie.BO.Web.MVC.Infrastracture.Mappings;
namespace Movie.BO.Web.MVC.Infrastracture.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.RegisterMaps(); 
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUserRolesService, UserRolesService>();
            services.AddScoped<IRoleManagerService, RoleManagerService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddRepositories();
        }
    }
}
