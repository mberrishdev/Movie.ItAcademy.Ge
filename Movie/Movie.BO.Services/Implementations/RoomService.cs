using Mapster;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Models;
using Movie.Data;
using Movie.Services.Abstractions;
using Movie.Services.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
{
    public class RoomService : IRoomService
    {
        public readonly IRoomRepository _roomRepository;
        public readonly IWebServices _webServices;

        public RoomService(IRoomRepository roomRepository,IWebServices webServices, IServerOptionService optionService)
        {
            _roomRepository = roomRepository;
            _webServices = webServices;
        }


        public async Task<Guid> AddRoomAsync(Room room)
        {
            room.Id = Guid.NewGuid();
            room.UserCount = 0;
            room.Status = RoomStatus.New.ToString();
            await _roomRepository.AddRoomAsync(room.Adapt<Domain.POCO.Room>());

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
