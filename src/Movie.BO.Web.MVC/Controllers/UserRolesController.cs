using Mapster;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Exceptions;
using Movie.BO.Services.Implementations;
using Movie.BO.Services.Models.User;
using Movie.BO.Web.MVC.Models.Account;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Web.MVC.Controllers
{

    [AutoValidateAntiforgeryToken]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public class UserRolesController : BaseController
    {
        private readonly IUserRolesService _userRolesService;

        public UserRolesController(IUserRolesService userRolesService, IAntiforgery antiForgery) : base(antiForgery)
        {
            _userRolesService = userRolesService;
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Index()
        {
            List<UserRoles> result = await _userRolesService.GetUserRolesAsync();

            return View(result.Adapt<List<UserRolesViewModel>>());
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Manage(Guid id)
        {
            var userName = string.Empty;
            (IdentityUser User, List<ManageUserRoles> ManageUserRole) result;

            ViewBag.userId = id.ToString();

            try
            {
                result = await _userRolesService.GetManageUserRolesAsync(id.ToString());
                userName = result.User.UserName;
            }
            catch (NotFoundException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("NotFound");
            }

            ViewBag.UserName = userName;

            return View(result.ManageUserRole);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserDTO> model, Guid id)
        {
            (UpdateRoleStatus Status, string Message) result;

            try
            {
                result = await _userRolesService.UpdateUserRoleAsync(model.Adapt<List<ManageUserRoles>>(), id.ToString());

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
