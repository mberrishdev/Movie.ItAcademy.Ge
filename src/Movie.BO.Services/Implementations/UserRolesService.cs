using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Exceptions;
using Movie.BO.Services.Models.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
{
    public class UserRolesService : IUserRolesService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<UserRoles>> GetUserRolesAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRoles = new List<UserRoles>();

            foreach (IdentityUser user in users)
            {
                userRoles.Add(new UserRoles
                {
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = await GetUserRoles(user)
                });
            }

            return userRoles;
        }

        public async Task<(IdentityUser User, List<ManageUserRoles> ManageUserRole)> GetManageUserRolesAsync(string userId)
        {
            List<string> userRoles = new List<string>();
            IdentityUser user;
            try
            {
                user = await FindUserAsync(userId);
                userRoles = await GetUserRoles(user);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }

            var model = new List<ManageUserRoles>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRoles
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (userRoles.Contains(role.ToString()))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }

            return (user, model);
        }

        public async Task<(UpdateRoleStatus Status, string Message)> UpdateUserRoleAsync(List<ManageUserRoles> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with Id = {userId} cannot be found");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                return (UpdateRoleStatus.Failur, "Cannot remove user existing roles");
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                return (UpdateRoleStatus.Failur, "Cannot add selected roles to user");
            }

            return (UpdateRoleStatus.Succeess, "");
        }
        private async Task<List<string>> GetUserRoles(IdentityUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        private async Task<IdentityUser> FindUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with Id = {userId} cannot be found");
            }

            return new IdentityUser()
            {
                Email = user.Email,
                UserName = user.UserName,
            };
        }
    }

    public enum UpdateRoleStatus
    {
        Succeess,
        Failur
    }
}
