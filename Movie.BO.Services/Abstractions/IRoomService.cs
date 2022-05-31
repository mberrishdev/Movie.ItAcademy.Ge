using Movie.BO.Services.Models;
using Movie.Services.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IRoomService
    {
        Task<Room> GetRoomAsync(Guid id);
        Task<Room> GetRoomWithMovieAsync(Guid id);
        Task<List<Room>> GetAllRoomWithMovieAsync();
        Task<Guid> AddRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(Guid id);
        Task ChangeRoomStatusAsync(Guid id, RoomStatus newStatus);
    }
}
