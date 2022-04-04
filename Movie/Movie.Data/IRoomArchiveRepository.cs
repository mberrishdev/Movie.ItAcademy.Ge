using Movie.Domain.POCO;
using System.Threading.Tasks;

namespace Movie.Data
{
    public interface IRoomArchiveRepository
    {
        Task AddRoomArchiveAsync(RoomArchive roomArchive);
    }
}
