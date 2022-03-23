using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data
{
    public interface IServerOptionRepository
    {
        Task<ServerOption> GetOptionAsync(string optionKey);
    }
}
