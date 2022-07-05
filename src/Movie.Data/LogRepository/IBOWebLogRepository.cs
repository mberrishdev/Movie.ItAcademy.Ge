using Movie.Domain.POCO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Data.LogRepository
{
    public interface IBOWebLogRepository
    {
        Task<List<BOWebLog>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
