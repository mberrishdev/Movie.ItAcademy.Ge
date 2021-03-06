using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Movie.BO.Services.Exceptions;
using Movie.Services.Abstractions;
using Movie.Services.Enums;
using Movie.Services.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AccountService> _logger;

        public AccountService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IEnumerable<IdentityError>> RegisterAsync(RegisterModel model, Role role = Role.Moderator)
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
            var userRoles = await GetUserRoles(user);

            if (user != null && (userRoles.Contains(Role.Admin.ToString()) || userRoles.Contains(Role.Moderator.ToString())))
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (signInResult.Succeeded)
                {
                    var claims = new[] {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, userRoles.Contains(Role.Admin.ToString())? "Admin" : "Moderator"),
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim(ClaimTypes.NameIdentifier ,user.Id)
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

                return SignInStatus.Failure;
            }

            throw new AccessDeniedException($"{user.UserName} doesn't have access to BackOffice");
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

        private async Task<List<string>> GetUserRoles(IdentityUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }


}
