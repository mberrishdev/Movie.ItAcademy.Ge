using Movie.BO.Services.Models;
using Movie.Services.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllBookingsAsync();
        Task ChangeBookingStatus(Guid id, BookingStatus bookingStatus);
    }
}
