using Movie.BO.Services.Models;
using Movie.Services.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IRoomService
    {
        Task<Room> GetRoomAsync(Guid id);
        Task<Room> GetRoomWithMovieAsync(Guid id);
        Task<List<Room>> GetAllRoomsAsync();
        Task AddRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(Guid id);
        Task ChangeRoomStatusAsync(Guid id, RoomStatus newStatus);
    }
}
