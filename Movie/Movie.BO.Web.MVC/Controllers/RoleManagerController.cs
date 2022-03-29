using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Movie.BO.Web.MVC.Controllers
{

    [AutoValidateAntiforgeryToken]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public class RoleManagerController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleManagerController(RoleManager<IdentityRole> roleManager, IAntiforgery antiForgery) : base(antiForgery)
        {
            _roleManager = roleManager;
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }

            return RedirectToAction("Index");
        }
    }
}
