using Movie.BO.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IServerOptionService
    {
        Task<ServerOption> GetOptionAsync(string optionKey);
    }
}
