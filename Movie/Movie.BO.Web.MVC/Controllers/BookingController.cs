using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.BO.Services;
using Movie.BO.Services.Abstractions;
using Movie.BO.Web.MVC.Models;
using Movie.BO.Web.MVC.Models.Room;
using Movie.Services.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movie.BO.Services.Models;
namespace Movie.BO.Web.MVC.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class BookingController : Controller
    {
        public readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<IActionResult> Index()
        {
            List<Booking> result = await _bookingService.GetAlActiveBookingsAsync();

            if (result == null)
                return NotFound();

            var rooms = result.Adapt<List<BookingDTO>>();

            return View(rooms);
        }


        public async Task<IActionResult> CancellBooking(Guid id)
        {
            await _bookingService.ChangeBookingStatus(id, BookingStatus.CancelledByModerator);
            return RedirectToAction("Index");
        }

       
    }
}
