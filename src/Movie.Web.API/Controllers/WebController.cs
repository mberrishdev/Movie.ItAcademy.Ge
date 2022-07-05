using Microsoft.AspNetCore.Mvc;
using Movie.Services.Abstractions;
using Movie.Web.Services.Abstractions;
using System.Threading.Tasks;

namespace Movie.Web.MVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebController : ControllerBase
    {
        public readonly IRoomService _roomService;
        public WebController(IRoomService roomService, IAccountService accountController)
        {
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<IActionResult> RelodeData()
        {
            await _roomService.RelodeDataAsync();
            return Ok();
        }

        [HttpPost("HealthCheck")]
        public async Task<IActionResult> HealthCheck()
        {
            await Task.Delay(10);
            return Ok();
        }
    }
}
