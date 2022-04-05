using Microsoft.AspNetCore.Identity;
using Movie.BO.Services.Implementations;
using Movie.BO.Services.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IUserRolesService
    {
        Task<List<UserRoles>> GetUserRolesAsync();
        Task<(IdentityUser User, List<ManageUserRoles> ManageUserRole)> GetManageUserRolesAsync(string userId);
        Task<(UpdateRoleStatus Status, string Message)> UpdateUserRoleAsync(List<ManageUserRoles> model, string userId);
    }
}
