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
        public async Task CheckAndArchiveRoom(MovieDBContext dBContext)
        {
            var rooms = await dBContext.Rooms.Include(x => x.Movie).ToListAsync();

            foreach (var room in rooms)
            {
                if (room.PremierTime < DateTime.UtcNow)
                {
                    await dBContext.RoomArchives.AddAsync(new RoomArchive()
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
                    dBContext.Rooms.Remove(await dBContext.Rooms.FirstAsync(rm => rm.Id == room.Id));
                    await dBContext.SaveChangesAsync();
                }

            }
        }

        public async Task CheckIfRoomHasMovie(MovieDBContext dBContext)
        {
            foreach (var room in await dBContext.Rooms.Include(x => x.Movie).ToListAsync())
            {
                if (room.Movie == null)
                {
                    dBContext.Rooms.Remove(await dBContext.Rooms.FirstAsync(rm => rm.Id == room.Id));
                    await dBContext.SaveChangesAsync();

                }
            }
        }

    }
}
