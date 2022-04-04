using Movie.Domain.POCO;
using System.Threading.Tasks;

namespace Movie.Data
{
    public interface IMessageLogRepository
    {
        Task AddAsync(MessageLog messageLog);
    }
}
