using Movie.Persistance.Context;
using Movie.Services.Models;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Abstractions
{
    public interface IServerOptionService
    {
        Task<ServerOption> GetOptionAsync(string optionKey, MovieDBContext dbContext);
    }
}
