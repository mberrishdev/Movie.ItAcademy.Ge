using Movie.Domain.Bookings;
using System;
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

        public async Task BookRoomAsync(Booking bookingModel)
        {
            await _baseRepository.AddAsync(bookingModel);
        }

        public async Task<bool> IsExistAsync(Guid bookingId)
        {
            return await _baseRepository.AnyAsync(booking => booking.Id == bookingId);
        }
    }
}
