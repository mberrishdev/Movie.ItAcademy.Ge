using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Movie.BO.Services.CustomHealthChecks;
using Movie.BO.Web.MVC.Infrastracture.Extensions;
using Movie.Persistance.Context;


namespace Movie.BO.Web.MVC.Infrastracture.StartupConfiguration
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
                option.Events = new CookieAuthenticationEvents
                {
                    OnSignedIn = async ctx =>
                    {
                        ctx.HttpContext.User = ctx.Principal;
                    }
                };
            });

            services.AddHealthChecks()
                  .AddDbContextCheck<MovieDBContext>()
                  .AddCheck<WebApiHC>("API Service");

            services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(5);
                options.MaximumHistoryEntriesPerEndpoint(60);
                options.SetApiMaxActiveRequests(1);
                options.AddHealthCheckEndpoint("Check App Health", "/healthz");
            }).AddInMemoryStorage();

            services.AddHealthChecks();

            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "AntiforgeryFieldname";
                options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
                options.SuppressXFrameOptionsHeader = false;
            });

            services.AddControllersWithViews(options =>
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

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
                });
            });

            return builder;
        }
    }
}
