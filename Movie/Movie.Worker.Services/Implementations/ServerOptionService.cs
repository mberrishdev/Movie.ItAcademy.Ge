using Mapster;
using Microsoft.EntityFrameworkCore;
using Movie.Persistance.Context;
using Movie.Services.Models;
using Movie.Worker.Services.Abstractions;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class ServerOptionService : IServerOptionService
    {
        private IBaseRepository _repository;

        public async Task<ServerOption> GetOptionAsync(string optionKey, MovieDBContext dBContext)
        {
            _repository = new BaseRepository(dBContext);

            var result = await _repository.FirstOrDefaultAsync<Domain.POCO.ServerOption>(op => op.Key == optionKey);
            return result.Adapt<ServerOption>();
        }
    }
}
