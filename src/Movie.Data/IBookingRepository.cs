using Movie.Domain.Bookings;
using System;
using System.Threading.Tasks;

namespace Movie.Data
{
    public interface IBookingRepository
    {
        Task BookRoomAsync(Booking bookingModel);
        Task<bool> IsExistAsync(Guid bookingId);
    }
}
