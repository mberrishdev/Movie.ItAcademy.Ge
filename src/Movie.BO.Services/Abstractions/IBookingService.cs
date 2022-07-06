using Movie.Domain.Bookings;
using Movie.Domain.Bookings.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllBookingsAsync(CancellationToken cancellationToken);
        Task ChangeBookingStatus(ChangeBookingStatusCommand command, CancellationToken cancellationToken);
    }
}
