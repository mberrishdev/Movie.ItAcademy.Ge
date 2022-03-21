using Movie.Web.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Web.Services.Abstractions
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRoomWithMovieAsync();
        Task<Room> GetRoomAsync(Guid id);
        Task<Room> GetRoomWithMovieAsync(Guid id);
        Task IncreaseUserCountAsync(Guid roomId);
    }
}
