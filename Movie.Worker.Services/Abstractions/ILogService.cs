using Movie.Persistance.Context;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Abstractions
{
    public interface ILogService
    {
        Task CheckAndArchive(MovieDBContext dBContext);
    }
}
