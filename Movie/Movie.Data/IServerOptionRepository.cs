using Movie.Domain.POCO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Data
{
    public interface IServerOptionRepository
    {
        Task<ServerOption> GetOptionAsync(string optionKey);
        Task<List<ServerOption>> LoadAllOptions();
    }
}
