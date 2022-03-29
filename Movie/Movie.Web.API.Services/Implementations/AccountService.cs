using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Movie.Services.Enums;
using Movie.Services.Models;
using Movie.Web.API.Services.Abstractions;
using Movie.Web.API.Services.Exceptions;
using Movie.Web.API.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Web.API.Services.Implementations
{
    public class AccountService : Web.API.Services.Abstractions.IAccountService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IJwtService _jwtService;


        public AccountService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _jwtService = jwtService;
        }

        public async Task<IdentityUser> GetUserAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<JwtToken> LogInAsync(LogInModel model)
        {
            IdentityUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                throw new AuthenticateException($"username doesn't exist").AddApiError(400);
            var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (!signInResult.Succeeded)
                throw new AuthenticateException($"username or password is not correct").AddApiError(400);

            var userRoles = await GetUserRoles(user);

            if (!userRoles.Contains(Roles.User.ToString()))
                return null;

            return _jwtService.GenerateSecurityToken(model.UserName, Guid.Parse(user.Id), Roles.User);
        }

        public async Task<(List<IdentityError> , Guid)> RegisterAsync(RegisterModel model, Roles role = Roles.User)
        {
            string userName = model.UserName;
            var user = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                await _userManager.AddToRoleAsync(user, role.ToString());
                return (null, Guid.Parse(user.Id));
            }
            
            List<IdentityError> errors = result.Errors.ToList();

            return (errors, Guid.Empty);
        }

        private async Task<List<string>> GetUserRoles(IdentityUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }
}
