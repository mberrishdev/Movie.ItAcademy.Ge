using Movie.Persistance.Context;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Abstractions
{
    public interface IWebServices
    {
        Task RelodeWebData(MovieDBContext dBContext);
    }
}
