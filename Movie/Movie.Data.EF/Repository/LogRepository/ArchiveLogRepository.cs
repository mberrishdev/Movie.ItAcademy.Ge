using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data.LogRepository
{
    public class ArchiveLogRepository : IArchiveLogRepository
    {
        private readonly IBaseRepository<ArchiveLog> _baseRepository;

        public ArchiveLogRepository(IBaseRepository<ArchiveLog> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task AddArchiveLogAsync(ArchiveLog model)
        {
            await _baseRepository.AddAsync(model);
        }
    }
}
