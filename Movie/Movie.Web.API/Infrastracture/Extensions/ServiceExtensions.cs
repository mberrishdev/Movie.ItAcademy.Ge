﻿using Microsoft.Extensions.DependencyInjection;
using Movie.Services.Abstractions;
using Movie.Web.Services.Implementations;
using Movie.Web.Services.Abstractions;
using Movie.Web.API.Infrastracture.Mappings;

namespace Movie.Web.API.Infrastracture.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.RegisterMaps();
            services.AddSingleton<Services.Abstractions.IJwtService, Services.Implementations.JwtService>();

            services.AddScoped<Services.Abstractions.IAccountService, Services.Implementations.AccountService>();
            services.AddSingleton<IRoomService, RoomService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddRepositories();
        }
    }
}