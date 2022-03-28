using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data.EF.Repository
{
    public class MessageLogRepository : IMessageLogRepository
    {
        private readonly IBaseRepository<MessageLog> _baseRepository;

        public MessageLogRepository(IBaseRepository<MessageLog> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task AddAsync(MessageLog messageLog)
        {
            await _baseRepository.AddAsync(messageLog);
        }
    }
}
