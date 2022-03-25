
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Movie.Services.Abstractions;
using Movie.Services.Enums;
using Movie.Services.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movie.Web.Services.Implementations
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

        public async Task<IEnumerable<IdentityError>> RegisterAsync(RegisterModel model, Roles role = Roles.User)
        {

            string userName = model.UserName;
            var user = new IdentityUser
            {
                UserName = userName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                await _userManager.AddToRoleAsync(user, role.ToString());

                //var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
                //var result = await _userManager.CreateAsync(user, model.Password);

                //if (result.Succeeded)
                //{
                //    _logger.LogInformation("User created a new account with password.");
                //}
            }
            return result.Errors;
        }

        public async Task<SignInStatus> LoginAsync(LogInModel model, HttpContext httpContext)
        {
            IdentityUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (signInResult.Succeeded)
                {
                    var claims = new[] {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await httpContext.SignInAsync(
                                             CookieAuthenticationDefaults.AuthenticationScheme,
                                             new ClaimsPrincipal(identity),
                                             new AuthenticationProperties
                                             {
                                                 IsPersistent = model.RememberMe
                                             });

                    return SignInStatus.Success;
                }
            }


            return SignInStatus.Failure;
        }

        public async Task LogOutAsync(HttpContext httpContext)
        {
            await _signInManager.SignOutAsync();

            await httpContext.SignOutAsync(
                 CookieAuthenticationDefaults.AuthenticationScheme);

            _logger.LogInformation("User logged out.");
        }

        public async Task<IdentityUser> GetUserAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }
    }


}
