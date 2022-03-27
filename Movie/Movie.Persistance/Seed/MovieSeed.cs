using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movie.Domain.POCO;
using Movie.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movie.Persistance.Seed
{
    public static class MovieSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using var scope = serviceProvider.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<MovieDBContext>();

            Migrate(database);
            SeedEverything(database);
        }

        private static void SeedEverything(MovieDBContext context)
        {
            var seeded = false;

            SeedRoles(context, ref seeded);
            SeedAdmin(context, ref seeded);
            SeedServerOptions(context, ref seeded);

            if (seeded)
                context.SaveChanges();
        }

        private static void SeedRoles(MovieDBContext context, ref bool seeded)
        {
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin"
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Moderator"
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User"
                }
            };

            foreach (var role in roles)
            {
                if (!context.Roles.AnyAsync(x => x.Name == role.Name).Result)
                {
                    context.Roles.Add(role);
                    seeded = true;
                }
            }
        }

        private static void SeedAdmin(MovieDBContext context, ref bool seeded)
        {
            var adminUser = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                //Admin
                PasswordHash = "AJ3HJ65okdAa4LhT7iEE4iUcqhcJEEl6Fze13EoC+60O4ncWdrD1v4ZDLua2k2w1oA=="
            };

            context.Users.Add(adminUser);

            var adminRoleId = context.Roles.FirstOrDefault(role => role.Name == "Admin").Id;

            var userRole = new IdentityUserRole<string>()
            {
                UserId = adminUser.Id,
                RoleId = adminRoleId,
            };

            if (context.UserRoles.AnyAsync(x => x.UserId == userRole.UserId && x.RoleId == userRole.RoleId).Result)
            {
                context.UserRoles.Add(userRole);
                seeded = true;
            }
        }

        private static void SeedServerOptions(MovieDBContext context, ref bool seeded)
        {
            var serverOptions = new List<ServerOption>()
            {
                new ServerOption()
                {
                    Id=Guid.NewGuid(),
                    Key="move.worker.room.archiver.int.time.sec",
                    Value="60",
                },
                new ServerOption()
                {
                    Id=Guid.NewGuid(),
                    Key="move.web.mvc.relode",
                    Value="https://localhost:44355/Web",
                },
                new ServerOption()
                {
                    Id=Guid.NewGuid(),
                    Key="move.web.api.domain",
                    Value="https://localhost:44355/",
                },
                new ServerOption()
                {
                    Id=Guid.NewGuid(),
                    Key="move.worker.web.data.relode.int.time.sec",
                    Value="https://3600:44355/",
                },
                new ServerOption()
                {
                    Id=Guid.NewGuid(),
                    Key="move.web.api.relode",
                    Value="https://localhost:44355/api/Web",
                },
                new ServerOption()
                {
                    Id=Guid.NewGuid(),
                    Key="move.worker.room.checker.int.time.sec",
                    Value="50",
                },
                new ServerOption()
                {
                    Id=Guid.NewGuid(),
                    Key="move.web.mvc.domain",
                    Value="https://localhost:44344/",
                },
                new ServerOption()
                {
                    Id=Guid.NewGuid(),
                    Key="move.worker.booking.canceller.int.time.sec",
                    Value="5",
                },
            };

            foreach (var serverOption in serverOptions)
            {
                if (!context.ServerOptions.AnyAsync(x => x.Key == serverOption.Key).Result)
                {
                    context.ServerOptions.Add(serverOption);
                    seeded = true;
                }
            }
        }

        private static void Migrate(MovieDBContext context)
        {
            context.Database.Migrate();
        }
    }
}
