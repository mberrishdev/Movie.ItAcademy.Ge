using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Movie.BO.Web.MVC.Infrastracture.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection service)
        {
            TypeAdapterConfig<Models.Movie.MovieCreateModel, Services.Models.Movie>
            .NewConfig();

            TypeAdapterConfig<Models.Movie.MovieDTO, Services.Models.Movie>
            .NewConfig()
            .TwoWays();

            TypeAdapterConfig<Services.Models.Movie, Domain.POCO.Movie>
            .NewConfig()
            .TwoWays();

            TypeAdapterConfig<Models.Account.UserRolesViewModel, Services.Models.User.UserRoles>
            .NewConfig()
            .TwoWays();

            //TypeAdapterConfig<Models.UserDTO, Services.Models.User.User>
            //.NewConfig()
            //.TwoWays();
        }
    }
}
