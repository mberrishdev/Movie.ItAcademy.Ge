using Movie.Data.LogRepository;
using Movie.Domain.POCO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Data.EF.LogRepository
{
    public class BOWebLogRepository : IBOWebLogRepository
    {
        private readonly IBaseRepository<BOWebLog> _baseRepository;

        public BOWebLogRepository(IBaseRepository<BOWebLog> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await _baseRepository.RemoveAsync(id);
        }
        public async Task<List<BOWebLog>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }
    }
}
