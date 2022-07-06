using Mapster;
using Movie.BO.Services.Abstractions;
using Movie.Data;
using Movie.Domain.Rooms;
using Movie.Domain.Rooms.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IBaseRepository<Room> _baseRepository;
        public readonly IWebServices _webServices;

        public RoomService(WebServices webServices, IBaseRepository<Room> baseRepository)
        {
            _webServices = webServices;
            _baseRepository = baseRepository;
        }


        public async Task<int> AddRoomAsync(CreateRoomCommand command)
        {
            var room = new Room(command);
            await _baseRepository.AddAsync(room);

            //Relode web data
            await _webServices.RelodeWebData();
            return room.Id;
        }

        public async Task<Room> GetRoomWithMovieAsync(Guid id)
        {
            Domain.POCO.Room result = await _roomRepository.GetRoomWithMovieAsync(id);

            return result.Adapt<Room>();
        }

        public async Task<List<Room>> GetAllRoomWithMovieAsync()
        {
            List<Domain.POCO.Room> result = await _roomRepository.GetAllRoomWithMovieAsync();

            return result.Adapt<List<Room>>();
        }

        public async Task ChangeRoomStatusAsync(Guid id, RoomStatus newStatus)
        {
            await _roomRepository.ChangeRoomStatusAsync(id, newStatus.ToString());

            //Relode web data
            await _webServices.RelodeWebData();
        }

        public async Task DeleteRoomAsync(Guid id)
        {
            var room = await _roomRepository.GetRoomAsync(id);
            await _roomRepository.DeleteRoomAsync(room);

            //Relode web data
            await _webServices.RelodeWebData();
        }
        public async Task UpdateRoomAsync(Room room)
        {
            room.Movie.RoomId = room.Id;
            await _roomRepository.UpdateRoomAsync(room.Adapt<Domain.POCO.Room>());

            //Relode web data
            await _webServices.RelodeWebData();
        }

        public Task<Room> GetRoomAsync(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
