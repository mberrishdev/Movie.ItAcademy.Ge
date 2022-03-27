using Movie.Data;
using Movie.Domain.POCO;
using Movie.Worker.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class RoomService : IRoomService
    {
        public readonly IRoomRepository _roomRepository;
        public readonly IRoomArchiveRepository _roomArchiveRepository;


        public RoomService(IRoomRepository roomRepository, IRoomArchiveRepository roomArchiveRepository)
        {
            _roomRepository = roomRepository;
            _roomArchiveRepository = roomArchiveRepository;
        }

        public async Task CheckAndArchiveRoom()
        {
            var rooms = await _roomRepository.GetAllRoomWithMovieAsync();

            foreach (var room in rooms)
            {
                if (room.PremierTime < DateTime.UtcNow) 
                {
                    await _roomArchiveRepository.AddRoomArchiveAsync(new RoomArchive()
                    {
                        Id = Guid.NewGuid(),
                        RoomId = room.Id,
                        PremieredTime = room.PremierTime,
                        RoomUserCapacity = room.RoomUserCapacity,
                        UserCount = room.UserCount,
                        Price = room.Price,
                        Currency = room.Currency,
                        Name = room.Movie.Name,
                        Gener = room.Movie.Gener,
                        Director = room.Movie.Director,
                        IMDBReiting = room.Movie.IMDBReiting,
                        Description = room.Movie.Description,
                        BannerUrl = room.Movie.BannerUrl,
                    });

                    //Delete room
                    await _roomRepository.DeleteRoomAsync(await _roomRepository.GetRoomAsync(room.Id));
                }

            }
        }

        public async Task CheckIfRoomHasMovie()
        {
            foreach (var room in await _roomRepository.GetAllRoomWithMovieAsync())
                if (room.Movie == null)
                    await _roomRepository.DeleteRoomAsync(await _roomRepository.GetRoomAsync(room.Id));
        }
    }
}
