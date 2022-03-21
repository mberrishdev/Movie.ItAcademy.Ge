using Microsoft.AspNetCore.Identity;
using Movie.Services.Enums;
using Movie.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Services.Abstractions
{
    public interface IAccountService
    {
        Task<IEnumerable<IdentityError>> RegisterAsync(RegisterModel model);
        Task<(SignInStatus Status, string Email)> LoginAsync(LogInModel model);
        Task LogOutAsync();
    }
}
