using Microsoft.AspNetCore.Identity;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Models.User;
using Movie.Services.Models;
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

        public async Task<List<IdentityUser>> GetMovieUsersAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("User");

            return users.Select(user => new IdentityUser()
            {
                UserName = user.UserName,
                Id = user.Id,
                Email = user.Email
            }).ToList();
        }
    }
}
