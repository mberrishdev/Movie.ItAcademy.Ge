﻿using Movie.Services.Models;
using System.Threading.Tasks;

namespace Movie.Services.Abstractions
{
    public interface IServerOptionService
    {
        ServerOption GetOption(string optionKey);
        Task LoadServerOptions();
    }
}