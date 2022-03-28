using Mapster;
using Movie.Data;
using Movie.Web.Services.Abstractions;
using Movie.Web.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Web.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private Dictionary<Guid, Room> _RoomWithMovieData = new Dictionary<Guid, Room>();


        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<Room>> GetAllRoomWithMovieAsync(bool forceReload = false)
        {
            if (forceReload || !_RoomWithMovieData.Any())
                await RelodeDataAsync();

            return _RoomWithMovieData.Select(rm => rm.Value).ToList();
        }

        public async Task<Room> GetRoomAsync(Guid id, bool forceReload = false)
        {
            if (forceReload || !_RoomWithMovieData.Any())
                await RelodeDataAsync();

            return _RoomWithMovieData.ContainsKey(id) ? _RoomWithMovieData[id] : null;
        }

        public async Task<Room> GetRoomWithMovieAsync(Guid id, bool forceReload = false)
        {
            if (forceReload || !_RoomWithMovieData.ContainsKey(id))
                await RelodeDataAsync();

            return _RoomWithMovieData.ContainsKey(id) ? _RoomWithMovieData[id] : null;
        }

        public async Task IncreaseUserCountAsync(Guid roomId)
        {
            await _roomRepository.IncreaseUserCountAsync(roomId);
            await RelodeDataAsync();
        }

        public async Task RelodeDataAsync()
        {
            List<Domain.POCO.Room> roomsWithMovie = await _roomRepository.GetAllRoomWithMovieAsync();
            _RoomWithMovieData = roomsWithMovie.ToDictionary(room => room.Id, room => room.Adapt<Room>());
        }
    }
}
