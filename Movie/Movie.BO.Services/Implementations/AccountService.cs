using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public AccountService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IEnumerable<IdentityError>> RegisterAsync(RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
            }

            return result.Errors;
        }

        public async Task<(SignInStatus Status, string Email)> LoginAsync(LogInModel model)
        {
            IdentityUser user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (signInResult.Succeeded)
                {
                    return (SignInStatus.Success, user.Email);
                }
            }


            return (SignInStatus.Failure, "");
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
        }
    }


}
