using Movie.Domain.POCO;
using System.Collections.Generic;
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
