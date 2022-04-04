using Microsoft.AspNetCore.Identity;
using Movie.Services.Enums;
using Movie.Services.Models;
using Movie.Web.API.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Web.API.Services.Abstractions
{
    public interface IAccountService
    {
        Task<(List<IdentityError>, Guid)> RegisterAsync(RegisterModel model, Role role = Role.User);
        Task<JwtToken> LogInAsync(LogInModel model);
        Task<IdentityUser> GetUserAsync(string userName);
    }
}
