using Movie.Domain.POCO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Data.LogRepository
{
    public class MVCWebLogRepository : IMVCWebLogRepository
    {
        private readonly IBaseRepository<MVCWebLog> _baseRepository;

        public MVCWebLogRepository(IBaseRepository<MVCWebLog> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await _baseRepository.RemoveAsync(id);
        }

        public async Task<List<MVCWebLog>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }
    }
}
