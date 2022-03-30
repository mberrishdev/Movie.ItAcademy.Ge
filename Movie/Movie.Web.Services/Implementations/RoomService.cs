using Mapster;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository, IMemoryCache memoryCache)
        {
            _roomRepository = roomRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<Room>> GetAllRoomWithMovieAsync(bool forceReload = false)
        {
            List<Room> rooms = new List<Room>();

            if (forceReload || !_memoryCache.TryGetValue("Rooms", out rooms))
                rooms = await RelodeDataAsync();

            return rooms;
        }

        public async Task<Room> GetRoomAsync(Guid id, bool forceReload = false)
        {
            List<Room> rooms = new List<Room>();

            if (forceReload || !_memoryCache.TryGetValue("Rooms", out rooms))
                rooms = await RelodeDataAsync();

            return rooms.FirstOrDefault(room => room.Id == id);
        }

        public async Task<Room> GetRoomWithMovieAsync(Guid id, bool forceReload = false)
        {
            List<Room> rooms = new List<Room>();

            if (forceReload || !_memoryCache.TryGetValue("Rooms", out rooms))
                rooms = await RelodeDataAsync();

            return rooms.FirstOrDefault(room => room.Id == id);
        }

        public async Task IncreaseUserCountAsync(Guid roomId)
        {
            await _roomRepository.IncreaseUserCountAsync(roomId);
            await RelodeDataAsync();
        }

        public async Task<List<Room>> RelodeDataAsync()
        {
            List<Domain.POCO.Room> roomsWithMovie = await _roomRepository.GetAllRoomWithMovieAsync();
            var rooms = roomsWithMovie.Adapt<List<Room>>();

            var cacheEntryOption = new MemoryCacheEntryOptions().
                SetAbsoluteExpiration(TimeSpan.FromSeconds(1800));

            _memoryCache.Set("Rooms", rooms, cacheEntryOption);

            return rooms;
        }
    }
}
