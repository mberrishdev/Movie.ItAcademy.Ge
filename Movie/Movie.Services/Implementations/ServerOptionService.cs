using Mapster;
using Movie.Data;
using Movie.Services.Abstractions;
using Movie.Services.Models;
using System.Threading.Tasks;

namespace Movie.Services.Implementations
{
    public class ServerOptionService : IServerOptionService
    {
        public readonly IServerOptionRepository _serverOptionRepository;

        public ServerOptionService(IServerOptionRepository serverOptionRepository)
        {
            _serverOptionRepository = serverOptionRepository;
        }

        public async Task<ServerOption> GetOptionAsync(string optionKey)
        {
            var result = await _serverOptionRepository.GetOptionAsync(optionKey);
            return result.Adapt<ServerOption>();
        }
    }
}
