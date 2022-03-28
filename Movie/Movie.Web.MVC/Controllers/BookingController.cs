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

        public BookingController(IBookingService bookingService, IAccountService accountService) : base(accountService)
        {
            _bookingService = bookingService;
        }

        public async Task<IActionResult> BookRoom(Guid id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account", 
                    new { id = id, returnAction = "BookRoom", returnController= "Booking" });
            }


            IdentityUser user = await GetUserAsync();

            await _bookingService.BookRoomAsync(id, new Guid(user.Id));

            return RedirectToAction("Index","Room");
        }

    }
}
