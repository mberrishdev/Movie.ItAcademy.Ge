using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie.Services.Abstractions;
using Movie.Web.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace Movie.Web.MVC.Controllers
{
    public class BookingController : BaseController
    {
        public readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService, IAccountService accountController) : base(accountController)
        {
            _bookingService = bookingService;
        }

        [Authorize]
        public async Task<IActionResult> BookRoom(Guid id)
        {
            IdentityUser user = await GetUserAsync();

            await _bookingService.BookRoomAsync(id, new Guid(user.Id));

            return RedirectToAction("Index","Room");
        }

    }
}
