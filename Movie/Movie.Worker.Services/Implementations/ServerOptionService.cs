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
        public async Task<ServerOption> GetOptionAsync(string optionKey, MovieDBContext dbContext)
        {
            var result = await dbContext.ServerOptions.FirstOrDefaultAsync(op => op.Key == optionKey);
            return result.Adapt<ServerOption>();
        }
    }
}
