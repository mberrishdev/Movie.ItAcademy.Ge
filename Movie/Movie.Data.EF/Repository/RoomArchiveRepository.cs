using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data.EF.Repository
{
    internal class RoomArchiveRepository : IRoomArchiveRepository
    {
        private readonly IBaseRepository<RoomArchive> _baseRepository;

        public RoomArchiveRepository(IBaseRepository<RoomArchive> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task AddRoomArchiveAsync(RoomArchive roomArchive)
        {
           await _baseRepository.AddAsync(roomArchive);
        }
    }
}
