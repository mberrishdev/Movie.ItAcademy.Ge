using Mapster;
using Movie.Data;
using Movie.Services.Abstractions;
using Movie.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Services.Implementations
{
    public class ServerOptionService : IServerOptionService
    {
        public readonly IServerOptionRepository _serverOptionRepository;
        public Dictionary<string, ServerOption> serverOptins = new Dictionary<string, ServerOption>();

        public ServerOptionService(IServerOptionRepository serverOptionRepository)
        {
            _serverOptionRepository = serverOptionRepository;
        }

        public ServerOption GetOption(string optionKey)
        {
            return serverOptins.ContainsKey(optionKey) ? serverOptins[optionKey] : null;
        }

        public async Task<ServerOption> GetOptionAsync(string optionKey)
        {
            var result = await _serverOptionRepository.GetOptionAsync(optionKey);
            return result.Adapt<ServerOption>();
        }

        public async Task LoadServerOptions()
        {

            var serverOptions = await _serverOptionRepository.LoadAllOptions();
            List<ServerOption> options = serverOptions.Adapt<List<ServerOption>>();
            serverOptins.Clear();

            foreach (var option in options)
            {
                serverOptins.Add(option.Key, option);
            }
        }
    }
}
