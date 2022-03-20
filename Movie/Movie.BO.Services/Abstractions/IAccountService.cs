using Microsoft.AspNetCore.Identity;
using Movie.BO.Services.Implementations;
using Movie.BO.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IAccountService
    {
        Task<IEnumerable<IdentityError>> RegisterAsync(RegisterModel model);
        Task<SignInStatus> LoginAsync(LogInModel model);
        Task LogOutAsync();
    }

    public enum SignInStatus
    {
        Success,
        Failure
    }
}
