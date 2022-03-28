using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data
{
    public interface IMessageQueueRepository
    {
        Task AddIntoQueueAsync(MessageQueue messageQueue);
        Task<List<MessageQueue>> GetAllASCAsync();
        Task Delete(MessageQueue messageQueue);
    }
}
