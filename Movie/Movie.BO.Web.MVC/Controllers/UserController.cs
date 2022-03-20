using Mapster;
using Microsoft.AspNetCore.Mvc;
using Movie.BO.Services.Abstractions;
using Movie.BO.Web.MVC.Models.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Web.MVC.Controllers
{
    public class UserController : Controller
    {
        public readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            List<Services.Models.User.User> result = await _userService.GetMovieUsersAsync();

            if (result == null)
                return NotFound();

            var users = result.Adapt<List<UserDTO>>();

            return View(users);
        }
    }
}
