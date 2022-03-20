using Microsoft.EntityFrameworkCore;
using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
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
            return await _baseRepository.Table.Include(x => x.Movie).FirstAsync();
        }
        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task ChangeRoomStatusAsync(Guid id, string newStatus)
        {
            var movie = await _baseRepository.Table.FirstOrDefaultAsync(room => room.Id == id);
            movie.Status = newStatus;
            await _baseRepository.UpdateAsync(movie);
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
