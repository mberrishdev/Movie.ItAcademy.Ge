﻿using Mapster;
using Microsoft.AspNetCore.Mvc;
using Movie.Services.Abstractions;
using Movie.Services.Enums;
using Movie.Web.MVC.Models.Account;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Web.MVC.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) : base(accountService)
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

        public IActionResult LogOut()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _accountService.RegisterAsync(model.Adapt<Movie.Services.Models.RegisterModel>());

            if (!result.Any())
                return RedirectToAction("Login");

            foreach (var error in result)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromForm] LogInModel model)
        {

            if (!ModelState.IsValid)
                return View();


            SignInStatus status = await _accountService.LoginAsync(model.Adapt<Movie.Services.Models.LogInModel>(), HttpContext);

            if (status == SignInStatus.Success)
                return RedirectToAction("Index", "Room");


            ModelState.AddModelError("", "Username or password is incorrect");

            return View();
        }

        [HttpPost]
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