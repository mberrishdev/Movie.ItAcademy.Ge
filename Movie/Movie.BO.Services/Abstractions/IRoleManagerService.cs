using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IRoleManagerService
    {
        Task<List<IdentityRole>> GetRolesAsync();
        Task AddRoleAsync(string roleName);
    }
}
