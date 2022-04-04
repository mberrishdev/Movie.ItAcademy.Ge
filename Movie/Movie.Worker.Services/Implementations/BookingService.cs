using Movie.Domain.POCO;
using Movie.Persistance.Context;
using Movie.Services.Enums;
using Movie.Worker.Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private IBaseRepository _repository;

        public async Task CheckAndCancellBookings(MovieDBContext dBContext)
        {
            _repository = new BaseRepository(dBContext);

            System.Collections.Generic.List<Booking> activeBookings = await _repository
                .WhereAsync<Booking>(booking => booking.Status == "Active");

            System.Collections.Generic.List<Room> rooms = await _repository
                .GetAllAsync<Room>();

            ServerOption option = await _repository
                .FirstOrDefaultAsync<ServerOption>(op => op.Key == "move.booking.time.to.cancel.sec");

            int timeToCancelBooking = int.Parse(option.Value);

            foreach (Booking activeBooking in activeBookings)
            {
                if (activeBooking.PaymentStatus == PaymentStatus.Unpaid.ToString())
                {
                    Room room = rooms.FirstOrDefault(room => room.Id == activeBooking.RoomId);
                    if (room != null && (room.PremierTime - DateTime.UtcNow).TotalSeconds <= timeToCancelBooking)
                    {
                        await ChangeBookingStatusAsync(activeBooking.Id, BookingStatus.CancelledByWorker.ToString());
                    }
                }
            }
        }

        private async Task ChangeBookingStatusAsync(Guid id, string bookingStatus)
        {
            Booking booking = await _repository
                .FirstOrDefaultAsync<Booking>(booking => booking.Id == id);
            booking.Status = bookingStatus;

            await _repository.UpdateAsync<Booking>(booking);
        }
    }
}



