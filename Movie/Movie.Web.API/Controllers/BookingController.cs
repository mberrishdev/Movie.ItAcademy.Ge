using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie.Web.API.Services.Abstractions;
using Movie.Web.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace Movie.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : BaseController
    {
        public readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService, IAccountService accountService) :base(accountService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("BookRoom")]
        public async Task<IActionResult> BookRoom(Guid id)
        {
            IdentityUser user = await GetUserAsync();

            await _bookingService.BookRoomAsync(id, new Guid(user.Id));

            return Ok();
        }

    }
}
