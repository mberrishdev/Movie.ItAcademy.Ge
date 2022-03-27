using Microsoft.Extensions.DependencyInjection;
using Movie.Data;
using Movie.Data.EF;
using Movie.Data.EF.Repository;

namespace Movie.BO.Web.MVC.Infrastracture.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddSingleton<IServerOptionRepository, ServerOptionRepository>();

            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
