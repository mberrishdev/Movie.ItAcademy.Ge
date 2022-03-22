using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Exceptions;
using Movie.BO.Services.Implementations;
using Movie.BO.Services.Models.User;
using Movie.BO.Web.MVC.Models;
using Movie.BO.Web.MVC.Models.Account;
using Movie.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private readonly IUserRolesService _userRolesService;

        public UserRolesController(IUserRolesService userRolesService)
        {
            _userRolesService = userRolesService;
        }

        public async Task<IActionResult> Index()
        {
             List<UserRoles> result = await _userRolesService.GetUserRolesAsync();

            return View(result.Adapt<List<UserRolesViewModel>>());
        }

        public async Task<IActionResult> Manage(string userId)
        {
            UserDTO user;
            (IdentityUser User, List<ManageUserRoles> ManageUserRole) result;

            ViewBag.userId = userId;

            try
            {
                result = await _userRolesService.GetManageUserRolesAsync(userId);
                user = new UserDTO()
                {
                    Email = result.User.Email,
                    UserName = result.User.UserName,
                };
            }
            catch (NotFoundException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("NotFound");
            }

            ViewBag.UserName = user.UserName;
            
            return View(result.ManageUserRole);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            (UpdateRoleStatus Status, string Message) result;

            try
            {
                result = await _userRolesService.UpdateUserRoleAsync(model.Adapt<List<ManageUserRoles>>(),userId);

                if (result.Status == UpdateRoleStatus.Failur)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }
                return RedirectToAction("Index");

            }
            catch (NotFoundException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("NotFound");
            }
        }

    }
}
