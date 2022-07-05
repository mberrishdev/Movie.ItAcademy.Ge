using Movie.Domain.POCO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Data.LogRepository
{
    public interface IMVCWebLogRepository
    {
        Task<List<MVCWebLog>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
