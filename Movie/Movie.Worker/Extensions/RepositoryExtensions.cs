using Microsoft.Extensions.DependencyInjection;
using Movie.Data;
using Movie.Data.EF.LogRepository;
using Movie.Data.EF.Repository;
using Movie.Data.LogRepository;

namespace Movie.Worker.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomArchiveRepository, RoomArchiveRepository>();
            services.AddScoped<IBOWebLogRepository, BOWebLogRepository>();
            services.AddScoped<IMVCWebLogRepository, MVCWebLogRepository>();
            services.AddScoped<IAPIWebLogRepository, APIWebLogRepository>();

        }
    }
}
