using Mapster;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Models;
using Movie.BO.Web.MVC.Models;
using Movie.Domain.Booking.Commands;
using Movie.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.BO.Web.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Moderator")]
    public class BookingController : BaseController
    {
        public readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService, IAntiforgery antiForgery) : base(antiForgery)
        {
            _bookingService = bookingService;
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Index()
        {
            List<Booking> result = await _bookingService.GetAllBookingsAsync(CancellationToken.None);

            if (result == null)
                return RedirectToAction("Index", "NotFound");

            var rooms = result.Adapt<List<BookingViewModel>>();

            return View(rooms);
        }

        public async Task<IActionResult> CancellBooking(Guid id)
        {
            await _bookingService.ChangeBookingStatus(new ChangeBookingStatusCommand() { BookId = id, Status = BookingStatus.CancelledByModerator },
                cancellationToken: CancellationToken.None);
            return RedirectToAction("Index");
        }

    }
}
