using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Movie.BO.Web.MVC.Infrastracture;
using Movie.BO.Web.MVC.Infrastracture.Extensions;
using Movie.BO.Web.MVC.Infrastracture.Middlewares;
using Movie.Persistance.Context;
using Movie.Persistance.Seed;
using System;

namespace Movie.BO.Web.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

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

            //services.AddHealthChecks()
            //.AddCheck<ExampleHealthCheck>(
            //    "example_health_check",
            //    failureStatus: HealthStatus.Degraded,
            //    tags: new[] { "example" });


            //services.AddHealthChecks();
            //.AddSqlServer(Configuration.GetConnectionString("MovieDBContextConnection"));
            //services.AddHealthChecks();
            //services.AddHealthChecksUI()
             //   .AddSqlServerStorage(Configuration.GetConnectionString("MovieDBContextConnection"));
            // .AddUrlGroup("")



            services.AddAntiforgery(options =>
            {
                // Set Cookie properties using CookieBuilder properties†.
                options.FormFieldName = "AntiforgeryFieldname";
                options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
                options.SuppressXFrameOptionsHeader = false;
            });

            services.AddControllersWithViews(options =>
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services.AddMvc();

            services.AddServices();


            services.AddDbContext<MovieDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MovieDBContextConnection")),
                ServiceLifetime.Singleton);

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

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseServerOptionsLoaderMiddleware();

            //app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();
            app.UseAuthentication();

            var antiforgery = app.ApplicationServices.GetRequiredService<IAntiforgery>();

            //app.Use(async (context, next) =>
            //{
            //    var requestPath = context.Request.Path.Value;

            //    if (string.Equals(requestPath, "/", StringComparison.OrdinalIgnoreCase)
            //        || string.Equals(requestPath, "/index.html", StringComparison.OrdinalIgnoreCase))
            //    {
            //        var tokenSet = antiforgery.GetAndStoreTokens(context);
            //        context.Response.Cookies.Append("XSRF-TOKEN", tokenSet.RequestToken!,
            //            new CookieOptions { HttpOnly = false });
            //    }

            //    await next.Invoke();
            //});

            app.UseAuthorization();

            MovieSeed.Initialize(app.ApplicationServices);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseHealthChecks("/health", new HealthCheckOptions()
            //{
            //    Predicate = _ => true,
            //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //});

            //app.UseHealthChecksUI();
        }
    }
}
