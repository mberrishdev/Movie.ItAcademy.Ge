using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.BO.Web.MVC.Infrastracture.Extensions;
using Movie.BO.Web.MVC.Infrastracture.Middlewares;
using Movie.Persistance.Context;

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

            services.AddControllersWithViews();
            services.AddMvc();

            services.AddServices();


            services.AddDbContext<MovieDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MovieDBContextConnection"));
            });

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

           // app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}