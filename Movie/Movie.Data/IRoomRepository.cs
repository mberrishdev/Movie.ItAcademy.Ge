using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data
{
    public interface IRoomRepository
    {
        Task<Room> GetRoomAsync(Guid id);
        Task<List<Room>> GetAllRoomsAsync();
        Task<List<Room>> GetAllActiveRoomsAsync();
        Task<List<Room>> GetAllRoomWithMovieAsync();
        Task AddRoomAsync(Room movie);
        Task UpdateRoomAsync(Room movie);
        Task DeleteRoomAsync(Room movie);
        Task ChangeRoomStatusAsync(Guid id, string newStatus);
        Task<Room> GetRoomWithMovieAsync(Guid id);
        Task IncreaseUserCountAsync(Guid roomId);
    }
}
