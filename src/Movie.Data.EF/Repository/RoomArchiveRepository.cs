using Movie.Domain.POCO;
using System.Threading.Tasks;

namespace Movie.Data.EF.Repository
{
    public class RoomArchiveRepository : IRoomArchiveRepository
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
