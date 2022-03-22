using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Movie.Services.Abstractions;
using Movie.Web.MVC.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movie.Web.MVC.Controllers
{
    public class BaseController : Controller
    {
        private readonly IAccountService _accountService;

        public BaseController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        protected async Task<IdentityUser> GetUserAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userIdGuid = new Guid(userId);
            IdentityUser user = await _accountService.GetUserAsync(userIdGuid);

            return user;
        }
    }
}
