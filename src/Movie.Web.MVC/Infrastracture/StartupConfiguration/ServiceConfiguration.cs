using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Movie.Persistance.Context;
using Movie.Web.MVC.Infrastracture.Extensions;

namespace Movie.Web.MVC.Infrastracture.StartupConfiguration
{
    public static class ServiceConfiguration
    {
        public static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.LoginPath = new PathString("/Account/Login");
                option.AccessDeniedPath = new PathString("/Auth/AccessDenied");
            });

            services.AddControllersWithViews();

            services.AddMvc();
            services.AddMemoryCache();
            services.AddServices();

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<MovieDBContext>();

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("AspManager", policy =>
                {
                    policy.RequireRole("Test");
                    //policy.RequireClaim("Coding-Skill", "ASP.NET Core MVC");
                });
            });

            return builder;
        }
    }
}
