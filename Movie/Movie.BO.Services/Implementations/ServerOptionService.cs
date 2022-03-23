using Mapster;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Models;
using Movie.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
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
