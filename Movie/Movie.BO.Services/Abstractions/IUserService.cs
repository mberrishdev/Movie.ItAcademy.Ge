using Microsoft.AspNetCore.Identity;
using Movie.BO.Services.Models.User;
using Movie.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IUserService
    {
        Task<List<User>> GetMovieUsersAsync();
        Task<User> GetUserAsync(Guid id);
        Task UpdateUserAsync(IdentityUser user);
        Task DeleteUser(Guid id);
        Task<IEnumerable<IdentityError>> RegisterAsync(RegisterModel registerModel);
    }
}
