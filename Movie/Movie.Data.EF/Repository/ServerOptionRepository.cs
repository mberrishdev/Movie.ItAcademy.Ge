using Microsoft.EntityFrameworkCore;
using Movie.Domain.POCO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Data.EF.Repository
{
    public class ServerOptionRepository : IServerOptionRepository
    {
        private readonly IBaseRepository<ServerOption> _baseRepository;

        public ServerOptionRepository(IBaseRepository<ServerOption> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServerOption> GetOptionAsync(string optionKey)
        {
            return await _baseRepository.Table.FirstOrDefaultAsync(op => op.Key == optionKey);
        }

        public async Task<List<ServerOption>> LoadAllOptions()
        {
            return await _baseRepository.GetAllAsync();
        }
    }
}
