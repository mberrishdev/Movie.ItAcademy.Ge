using Movie.Web.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Web.Services.Abstractions
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRoomWithMovieAsync(bool forceReload = false);
        Task<Room> GetRoomAsync(Guid id, bool forceReload = false);
        Task<Room> GetRoomWithMovieAsync(Guid id, bool forceReload = false);
        Task IncreaseUserCountAsync(Guid roomId);
        Task<List<Room>> RelodeDataAsync();
    }
}
