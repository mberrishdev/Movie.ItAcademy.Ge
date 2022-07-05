using Mapster;
using Microsoft.Extensions.Caching.Memory;
using Movie.Data;
using Movie.Services.Abstractions;
using Movie.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Services.Implementations
{
    public class ServerOptionService : IServerOptionService
    {
        public readonly IServerOptionRepository _serverOptionRepository;
        private readonly IMemoryCache _memoryCache;

        public ServerOptionService(IServerOptionRepository serverOptionRepository, IMemoryCache memoryCache)
        {
            _serverOptionRepository = serverOptionRepository;
            _memoryCache = memoryCache;
        }


        public async Task<ServerOption> GetOptionAsync(string optionKey)
        {
            List<ServerOption> options = new List<ServerOption>();

            if (!_memoryCache.TryGetValue("Options", out options))
                options = await LoadServerOptions();

            var option = options.FirstOrDefault(op => op.Key == optionKey);

            if (option == null)
                options = await LoadServerOptions();

            return options.FirstOrDefault(op => op.Key == optionKey);
        }

        public async Task<List<ServerOption>> LoadServerOptions()
        {
            var serverOptions = await _serverOptionRepository.LoadAllOptions();
            List<ServerOption> options = serverOptions.Adapt<List<ServerOption>>();

            _memoryCache.Remove("Options");
            var cacheEntryOption = new MemoryCacheEntryOptions().
                SetAbsoluteExpiration(TimeSpan.FromSeconds(1800));

            _memoryCache.Set("Options", options, cacheEntryOption);

            return options;
        }
    }
}
