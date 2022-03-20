using Mapster;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Models;
using Movie.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
{
    public class RoomService : IRoomService
    {
        public readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }


        public async Task AddRoomAsync(Room room)
        {
            room.Id = Guid.NewGuid();
            room.UserCount = 0;
            room.Status = RoomStatus.New.ToString();
            await _roomRepository.AddRoomAsync(room.Adapt<Domain.POCO.Room>());
        }

        public async Task<Room> GetRoomWithMovieAsync(Guid id)
        {
            Domain.POCO.Room result =  await _roomRepository.GetRoomWithMovieAsync(id);

            return result.Adapt<Room>();
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            List<Domain.POCO.Room> result = await _roomRepository.GetAllRoomsAsync();

            return result.Adapt<List<Room>>();
        }

        public async Task ChangeRoomStatusAsync(Guid id, RoomStatus newStatus)
        {
            await _roomRepository.ChangeRoomStatusAsync(id, newStatus.ToString());
        }

        public async Task DeleteRoomAsync(Guid id)
        {
            var room = await _roomRepository.GetRoomAsync(id);
            await _roomRepository.DeleteRoomAsync(room);
        }

        public Task<Room> GetRoomAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRoomAsync(Room room)
        {
            throw new NotImplementedException();
        }
    }
}
