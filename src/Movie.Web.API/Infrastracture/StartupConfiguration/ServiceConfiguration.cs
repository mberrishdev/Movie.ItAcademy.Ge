using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Movie.Persistance.Context;
using Movie.Web.API.Infrastracture.Extensions;
using Movie.Web.API.Services.Models;
using System.Collections.Generic;

namespace Movie.Web.Api.Infrastracture.StartupConfiguration
{
    public static class ServiceConfiguration
    {
        public static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;
            var configuration = builder.Configuration;

            services.AddControllers();
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<MovieDBContext>();

            services.AddMemoryCache();
            services.AddServices();


            services.Configure<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Movie",
                    Version = "v1",
                    Description = "Api",
                });

                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>(){ }
                    }
                });
            });

            services.AddTokenAuthentication(configuration);

            return builder;
        }
    }
}
