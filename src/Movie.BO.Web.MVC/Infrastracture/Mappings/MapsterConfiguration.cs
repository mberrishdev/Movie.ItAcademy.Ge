using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Movie.BO.Services.Models;
using Movie.BO.Services.Models.User;
using Movie.BO.Web.MVC.Models;
using Movie.BO.Web.MVC.Models.Account;
using Movie.BO.Web.MVC.Models.Movie;
using Movie.BO.Web.MVC.Models.Room;
using Movie.Services.Models;


namespace Movie.BO.Web.MVC.Infrastracture.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection service)
        {
            //Account 
            TypeAdapterConfig<RegisterDTO, RegisterModel>
            .NewConfig();

            TypeAdapterConfig<LogInDTO, LogInModel>
            .NewConfig();

            //Booking
            TypeAdapterConfig<Booking, BookingViewModel>
            .NewConfig();

            //Movie
            TypeAdapterConfig<MovieDTO, Services.Models.Movie>
            .NewConfig();

            //Room
            TypeAdapterConfig<Room, RoomViewModel>
            .NewConfig();

            TypeAdapterConfig<Room, RoomWithMovieViewModel>
            .NewConfig();

            TypeAdapterConfig<RoomDTO, Room>
            .NewConfig();

            TypeAdapterConfig<RoomWithMovieDTO, Room>
            .NewConfig();

            //User Role
            TypeAdapterConfig<ManageUserDTO, ManageUserRoles>
            .NewConfig();

        }
    }
}
