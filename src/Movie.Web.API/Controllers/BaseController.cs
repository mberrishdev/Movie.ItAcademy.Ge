using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie.Web.API.Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Web.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IAccountService _accountService;

        public BaseController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        protected async Task<IdentityUser> GetUserAsync()
        {
            var userName = HttpContext.Request.HttpContext.User.Identities.FirstOrDefault()?.Name;

            if (userName == null)
                throw new NullReferenceException();

            var result = await _accountService.GetUserAsync(userName);

            return result;
        }
    }
}
