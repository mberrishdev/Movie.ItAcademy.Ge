using Microsoft.EntityFrameworkCore;
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
        public async Task CheckAndCancellBookings(MovieDBContext dBContext)
        {
            var activeBookings = await dBContext.Bookings.Where(booking => booking.Status == "Active")
                             .ToListAsync();

            var rooms = await dBContext.Rooms.ToListAsync();

            var option = await dBContext.ServerOptions.FirstOrDefaultAsync(op => op.Key == "move.booking.time.to.cancel.sec");

            var timeToCancelBooking = int.Parse(option.Value);

            foreach (var activeBooking in activeBookings)
            {
                if (activeBooking.PaymentStatus == PaymentStatus.Unpaid.ToString())
                {
                    var room = rooms.FirstOrDefault(room => room.Id == activeBooking.RoomId);
                    if (room != null && (room.PremierTime - DateTime.UtcNow).TotalSeconds <= timeToCancelBooking)
                        await ChangeBookingStatusAsync(activeBooking.Id, BookingStatus.CancelledByWorker.ToString(), dBContext);
                }
            }
        }

        private async Task ChangeBookingStatusAsync(Guid id, string bookingStatus, MovieDBContext dBContext)
        {
            var booking = await dBContext.Bookings.FirstOrDefaultAsync(booking => booking.Id == id);
            booking.Status = bookingStatus;

            dBContext.Bookings.Update(booking);

            await dBContext.SaveChangesAsync();
        }
    }
}



