using Mapster;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Exceptions;
using Movie.Data;
using Movie.Domain.Bookings;
using Movie.Domain.Bookings.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
{
    public class BookingService : IBookingService
    {
        public readonly IBookingRepository _bookingRepository;
        private readonly IBaseRepository<Booking> _baseRepository;

        public BookingService(IBaseRepository<Booking> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task ChangeBookingStatus(ChangeBookingStatusCommand command, CancellationToken cancellationToken)
        {
            var booking = await _baseRepository.GetAsync(command.BookId, cancellationToken: cancellationToken);

            if (booking == null)
                throw new NotFoundException($"Booking with id {command.BookId} was not found");

            booking.ChangeBookingStatus(command);

            await _baseRepository.UpdateAsync(booking, cancellationToken);
        }

        public async Task<List<Booking>> GetAllBookingsAsync(CancellationToken cancellationToken)
        {
            var result = await _baseRepository.GetAllAsync(cancellationToken);

            return result.Adapt<List<Booking>>();
        }
    }
}
