using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Movie.Services.Enums;
using Movie.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Services.Abstractions
{
    public interface IAccountService
    {
        Task<IEnumerable<IdentityError>> RegisterAsync(RegisterModel model);
        Task<SignInStatus> LoginAsync(LogInModel model, HttpContext httpContext);
        Task LogOutAsync(HttpContext httpContext);
        Task<IdentityUser> GetUserAsync(Guid id);
    }
}
