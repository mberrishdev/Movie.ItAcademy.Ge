using Movie.Domain.POCO;
using System.Threading.Tasks;

namespace Movie.Data.LogRepository
{
    public interface IArchiveLogRepository
    {
        Task AddArchiveLogAsync(ArchiveLog model);
    }
}
