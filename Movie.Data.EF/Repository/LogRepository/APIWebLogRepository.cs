using Movie.Data.LogRepository;
using Movie.Domain.POCO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Data.EF.LogRepository
{
    public class APIWebLogRepository : IAPIWebLogRepository
    {
        private readonly IBaseRepository<APIWebLog> _baseRepository;

        public APIWebLogRepository(IBaseRepository<APIWebLog> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await _baseRepository.RemoveAsync(id);
        }

        public async Task<List<APIWebLog>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }
    }
}
