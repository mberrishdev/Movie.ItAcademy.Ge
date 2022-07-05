using Microsoft.EntityFrameworkCore;
using Movie.Domain.POCO;
using Movie.Persistance.Context;
using Movie.Worker.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private IBaseRepository _repository;

        public async Task CheckAndArchiveRoom(MovieDBContext dBContext)
        {
            _repository = new BaseRepository(dBContext);

            System.Collections.Generic.List<Room> rooms = await dBContext.Rooms.Include(x => x.Movie).ToListAsync();

            foreach (Room room in rooms)
            {
                if (room.PremierTime < DateTime.UtcNow)
                {
                    await _repository.AddAsync<RoomArchive>(new RoomArchive()
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
                    await _repository.RemoveAsync<Room>(await _repository.FirstOrDefaultAsync<Room>(rm => rm.Id == room.Id));
                }

            }
        }

        public async Task CheckIfRoomHasMovie(MovieDBContext dBContext)
        {
            _repository = new BaseRepository(dBContext);

            foreach (Room room in await dBContext.Rooms.Include(x => x.Movie).ToListAsync())
            {
                if (room.Movie == null)
                {
                    await _repository.RemoveAsync<Room>(await _repository.FirstOrDefaultAsync<Room>(rm => rm.Id == room.Id));
                }
            }
        }

    }
}
