
using Microsoft.AspNetCore.Identity;
using Movie.Services.Enums;
using Movie.Services.Models;
using Movie.Web.API.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Web.API.Services.Abstractions
{
    public interface IAccountService
    {
        Task<Guid> RegisterAsync(RegisterModel model, Roles role = Roles.User);
        Task<JwtToken> LogInAsync(LogInModel model);
        Task<IdentityUser> GetUserAsync(string userName);
    }
}
