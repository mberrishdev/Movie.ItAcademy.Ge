using Microsoft.Extensions.DependencyInjection;
using Movie.Data;
using Movie.Data.EF;
using Movie.Data.EF.LogRepository;
using Movie.Data.EF.Repository;
using Movie.Data.LogRepository;

namespace Movie.Worker.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IServerOptionRepository, ServerOptionRepository>();
            services.AddSingleton<IBookingRepository, BookingRepository>();
            services.AddSingleton<IRoomRepository, RoomRepository>();
            services.AddSingleton<IRoomArchiveRepository, RoomArchiveRepository>();
            services.AddSingleton<IBOWebLogRepository, BOWebLogRepository>();
            services.AddSingleton<IMVCWebLogRepository, MVCWebLogRepository>();
            services.AddSingleton<IAPIWebLogRepository, APIWebLogRepository>();
            services.AddSingleton<IArchiveLogRepository, ArchiveLogRepository>();
            services.AddSingleton<IMessageQueueRepository, MessageQueueRepository>();
            services.AddSingleton<IMessageLogRepository, MessageLogRepository>();
            services.AddSingleton<IAspNetUserRepository, AspNetUserRepository>();


            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
