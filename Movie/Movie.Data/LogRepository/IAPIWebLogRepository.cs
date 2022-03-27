using Movie.Domain.POCO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Data.LogRepository
{
    public interface IAPIWebLogRepository
    {
        Task<List<APIWebLog>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
