using Mapster;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Movie.BO.Web.MVC.Models.Account;
using Movie.Services.Abstractions;
using Movie.Services.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.BO.Web.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    [ValidateAntiForgeryToken]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, IAntiforgery antiForgery):base(antiForgery)
        {
            _accountService = accountService;
        }

        [IgnoreAntiforgeryToken]
        public IActionResult Register()
        {
            return View();
        }

        [IgnoreAntiforgeryToken]
        public IActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _accountService.RegisterAsync(model.Adapt<Movie.Services.Models.RegisterModel>(), Roles.Moderator);

            if (!result.Any())
                return RedirectToAction("Login");

            foreach (var error in result)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LogInDTO model)
        {

            if (!ModelState.IsValid)
                return View();


            SignInStatus status = await _accountService.LoginAsync(model.Adapt<Movie.Services.Models.LogInModel>(), HttpContext);

            if (status == SignInStatus.Success)
                return RedirectToAction("", "");


            ModelState.AddModelError("", "Username or password is incorrect");

            return View();
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            await _accountService.LogOutAsync(HttpContext);
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("LogOut");
            }
        }
    }
}
