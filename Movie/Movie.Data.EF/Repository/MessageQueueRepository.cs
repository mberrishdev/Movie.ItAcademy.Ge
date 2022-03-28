using Microsoft.EntityFrameworkCore;
using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data.EF.Repository
{
    public class MessageQueueRepository : IMessageQueueRepository
    {
        private readonly IBaseRepository<MessageQueue> _baseRepository;

        public MessageQueueRepository(IBaseRepository<MessageQueue> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task AddIntoQueueAsync(MessageQueue messageQueue)
        {
            await _baseRepository.AddAsync(messageQueue);
        }

        public async Task Delete(MessageQueue messageQueue)
        {
            await _baseRepository.RemoveAsync(messageQueue);
        }

        public async Task<List<MessageQueue>> GetAllASCAsync()
        {
            return await _baseRepository.Table.OrderBy(mq => mq.Date).ToListAsync();
        }
    }
}
