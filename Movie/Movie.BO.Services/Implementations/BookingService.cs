using Mapster;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Models;
using Movie.Data;
using Movie.Services.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
{
    public class BookingService : IBookingService
    {
        public readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task ChangeBookingStatus(Guid id, BookingStatus bookingStatus)
        {
            await _bookingRepository.ChangeBookingStatusAsync(id, bookingStatus.ToString());
        }

        public async Task<List<Booking>> GetAlActiveBookingsAsync()
        {
            var result = await _bookingRepository.GetAlActiveBookingsAsync();

            return result.Adapt<List<Booking>>();
        }
    }
}
