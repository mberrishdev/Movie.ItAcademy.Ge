using Movie.Services.Models;
using System.Threading.Tasks;

namespace Movie.Services.Abstractions
{
    public interface IServerOptionService
    {
        Task<ServerOption> GetOptionAsync(string optionKey);
    }
}
