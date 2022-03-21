using Mapster;
using Movie.Data;
using Movie.Web.Services.Abstractions;
using Movie.Web.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Web.Services.Implementations
{
    public class RoomService : IRoomService
    {
        public readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<Room>> GetAllRoomWithMovieAsync()
        {
            List<Domain.POCO.Room> result = await _roomRepository.GetAllRoomWithMovieAsync();

            return result.Adapt<List<Room>>();
        }

        public async Task<Room> GetRoomAsync(Guid id)
        {
            Domain.POCO.Room room = await _roomRepository.GetRoomAsync(id);
            return room.Adapt<Room>();
        }

        public async Task<Room> GetRoomWithMovieAsync(Guid id)
        {
            Domain.POCO.Room result = await _roomRepository.GetRoomWithMovieAsync(id);

            return result.Adapt<Room>();
        }

        public async Task IncreaseUserCountAsync(Guid roomId)
        {
           await _roomRepository.IncreaseUserCountAsync(roomId);
        }
    }
}
