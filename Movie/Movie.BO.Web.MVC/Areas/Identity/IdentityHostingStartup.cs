//using System;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Movie.Persistance;

//[assembly: HostingStartup(typeof(Movie.BO.Web.MVC.Areas.Identity.IdentityHostingStartup))]
//namespace Movie.BO.Web.MVC.Areas.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) =>
//            {
//                services.AddDbContext<MovieDBContext>(options =>
//                    options.UseSqlServer(
//                        context.Configuration.GetConnectionString("MovieDBContextConnection")));

//                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                    .AddEntityFrameworkStores<MovieDBContext>();
//            });
//        }
//    }
//}