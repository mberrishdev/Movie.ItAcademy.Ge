using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Models.User;
using System;
using System.Collections.Generic;
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
            var users = await _userManager.Users.ToListAsync();
            var userWithRoles = new List<User>();

            foreach (IdentityUser user in users)
            {
                userWithRoles.Add(new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = await GetUserRoles(user)
                });
            }

            return userWithRoles;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return null;

            return new User()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = await GetUserRoles(user)
            };
        }



        public async Task UpdateUserAsync(IdentityUser user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUser(Guid id)
        {
            var user =  await GetIdentityUserAsync(id);
            await _userManager.DeleteAsync(user);
        }


        private async Task<List<string>> GetUserRoles(IdentityUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        private async Task<IdentityUser> GetIdentityUserAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }
    }
}
