using Mapster;
using Microsoft.AspNetCore.Mvc;
using Movie.Web.API.Models.Account;
using Movie.Web.API.Services.Abstractions;
using System.Threading.Tasks;

namespace Movie.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        public AccountController(IAccountService accountService) : base(accountService)
        {
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _accountService.RegisterAsync(model.Adapt<Movie.Services.Models.RegisterModel>());

            if(result.Item1 != null)
                return BadRequest(result.Item1);

            return Ok(result.Item2);
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> AuthenticateAccount(LogInModel model)
        {
            var result = await _accountService.LogInAsync(model.Adapt<Movie.Services.Models.LogInModel>());
            return Ok(result);
        }
    }
}
