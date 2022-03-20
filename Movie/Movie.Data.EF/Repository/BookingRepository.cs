using Microsoft.EntityFrameworkCore;
using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Data.EF.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IBaseRepository<Booking> _baseRepository;

        public BookingRepository(IBaseRepository<Booking> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task ChangeBookingStatus(Guid id, string bookingStatus)
        {
            var booking = await _baseRepository.Table.FirstOrDefaultAsync(booking => booking.Id == id);
            booking.Status = bookingStatus;
            await _baseRepository.UpdateAsync(booking);
        }

        public async Task<List<Booking>> GetAlActiveBookingsAsync()
        {
           return await _baseRepository.Table.Where(booking => booking.Status == "Active")
                                        .ToListAsync();
        }
    }
}
