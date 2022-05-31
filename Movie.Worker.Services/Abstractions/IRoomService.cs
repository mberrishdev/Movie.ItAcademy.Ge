using Movie.Persistance.Context;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Abstractions
{
    public interface IRoomService
    {
        Task CheckAndArchiveRoom(MovieDBContext dBContext);
        Task CheckIfRoomHasMovie(MovieDBContext dBContext);
    }
}
