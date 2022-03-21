using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAlActiveBookingsAsync();
        Task<List<Booking>> GetAllBookingsAsync();
        Task ChangeBookingStatusAsync(Guid id, string bookingStatus);
        Task BookRoomAsync(Booking bookingModel);
        Task<bool> IsExistAsync(Guid bookingId);
    }
}
