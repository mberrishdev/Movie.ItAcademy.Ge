using Microsoft.Extensions.DependencyInjection;
using Movie.Data;
using Movie.Data.EF;
using Movie.Data.EF.Repository;

namespace Movie.Web.API.Infrastracture.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddSingleton<IRoomRepository, RoomRepository>();
            services.AddSingleton<IServerOptionRepository, ServerOptionRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
