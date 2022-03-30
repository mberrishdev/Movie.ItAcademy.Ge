using Movie.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Services.Abstractions
{
    public interface IServerOptionService
    {
        Task<ServerOption> GetOptionAsync(string optionKey);
        Task<List<ServerOption>> LoadServerOptions();
    }
}
