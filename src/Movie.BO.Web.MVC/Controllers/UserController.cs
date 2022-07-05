using Mapster;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Models.User;
using Movie.BO.Web.MVC.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.BO.Web.MVC.Controllers
{

    [AutoValidateAntiforgeryToken]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController
    {
        public readonly IUserService _userService;

        public UserController(IUserService userService, IAntiforgery antiForgery) : base(antiForgery)
        {
            _userService = userService;
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Index()
        {
            List<User> result = await _userService.GetMovieUsersAsync();

            if (result == null)
                return RedirectToAction("Index", "NotFound");

            var users = result.Adapt<List<UserViewModel>>();

            return View(users);
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> RegisterUser()
        {
            return View();
        }

        public async Task<IActionResult> Update(Guid id)
        {

            User result = await _userService.GetUserAsync(id);

            if (result == null)
                return RedirectToAction("Index", "NotFound");

            var userViewModel = result.Adapt<UserViewModel>();

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserDTO model)
        {
            await _userService.UpdateUserAsync(new IdentityUser()
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userService.RegisterAsync(model.Adapt<Movie.Services.Models.RegisterModel>());

            if (!result.Any())
                return RedirectToAction("Index");

            foreach (var error in result)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteUser(id);

            return RedirectToAction("Index");
        }
    }
}
