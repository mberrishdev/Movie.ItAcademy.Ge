﻿using Mapster;
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
using Microsoft.AspNetCore.Antiforgery;

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

        public async Task<IActionResult> Index()
        {
            List<Booking> result = await _bookingService.GetAllBookingsAsync();

            if (result == null)
                return NotFound();

            var rooms = result.Adapt<List<BookingViewModel>>();

            return View(rooms);
        }

        public async Task<IActionResult> CancellBooking(Guid id)
        {
            await _bookingService.ChangeBookingStatus(id, BookingStatus.CancelledByModerator);
            return RedirectToAction("Index");
        }

    }
}
