using Microsoft.EntityFrameworkCore;
using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Data.EF.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IBaseRepository<Room> _baseRepository;

        public RoomRepository(IBaseRepository<Room> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task AddRoomAsync(Room movie)
        {
            await _baseRepository.AddAsync(movie);
        }

        public async Task<Room> GetRoomWithMovieAsync(Guid id)
        {
            return await _baseRepository.Table.Include(x => x.Movie).FirstAsync(r => r.Id == id);
        }
        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task<List<Room>> GetAllRoomWithMovieAsync()
        {
            return await _baseRepository.Table.Include(x => x.Movie).ToListAsync();
        }

        public async Task<List<Room>> GetAllActiveRoomsAsync()
        {
            return await _baseRepository.Table.Where(room => room.Status == "Active").ToListAsync();
        }

        public async Task ChangeRoomStatusAsync(Guid id, string newStatus)
        {
            var room = await _baseRepository.Table.FirstOrDefaultAsync(room => room.Id == id);
            room.Status = newStatus;
            await _baseRepository.UpdateAsync(room);
        }

        public async Task IncreaseUserCountAsync(Guid roomId)
        {
            var room = await _baseRepository.Table.FirstOrDefaultAsync(room => room.Id == roomId);
            room.UserCount++;

            if (room.RoomUserCapacity < room.UserCount)
                return;
            await _baseRepository.UpdateAsync(room);
        }

        public async Task DeleteRoomAsync(Room movie)
        {
            await _baseRepository.RemoveAsync(movie);
        }


        public Task<Room> GetRoomAsync(Guid id)
        {
            throw new NotImplementedException();
        }


        public Task UpdateRoomAsync(Room movie)
        {
            throw new NotImplementedException();
        }

    }
}
