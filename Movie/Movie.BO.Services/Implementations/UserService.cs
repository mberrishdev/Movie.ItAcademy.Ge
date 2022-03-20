using Microsoft.AspNetCore.Identity;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<User>> GetMovieUsersAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("User");

            return users.Select(user => new User()
            {
                UserName = user.UserName,
                Id = user.Id,
                Email = user.Email
            }).ToList();
        }
    }
}
