using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movie.BO.Services.Abstractions;
using Movie.BO.Web.MVC.Models.Account;
using System.Threading.Tasks;

namespace Movie.BO.Web.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _accountService.RegisterAsync(model.Adapt<Services.Models.User.RegisterModel>());

            foreach (var error in result)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LogInModel model)
        {
            if (!ModelState.IsValid)
                return View();


            SignInStatus result = await _accountService.LoginAsync(model.Adapt<Services.Models.User.LogInModel>());

            if (result == SignInStatus.Success)
                return RedirectToAction("", "");


            ModelState.AddModelError("", "Username or password is incorrect");

            return View();    
        }

        public async Task<IActionResult> LogOut()
        {
            await _accountService.LogOutAsync();
            return RedirectToAction("LogIn");
        }
    }
}
