using Microsoft.AspNetCore.Identity;
using Movie.BO.Services.Models.User;
using Movie.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IUserService
    {
        Task<List<IdentityUser>> GetMovieUsersAsync();
    }
}
