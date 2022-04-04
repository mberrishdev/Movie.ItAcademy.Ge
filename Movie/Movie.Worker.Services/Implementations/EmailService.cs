using Microsoft.EntityFrameworkCore;
using Movie.Data;
using Movie.Domain.POCO;
using Movie.Persistance.Context;
using Movie.Services.Enums;
using Movie.Worker.Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public async Task CheckAndSendEmail(MovieDBContext dBContext)
        {

            var activeBookings = await dBContext.Bookings.Where(booking => booking.Status == "Active").ToListAsync();
            var users = await dBContext.Users.ToListAsync();
            var rooms = await dBContext.Rooms.ToListAsync();

            var option = await dBContext.ServerOptions.FirstOrDefaultAsync(op => op.Key == "move.booking.time.to.remainder.email.sec");
            var timeToRemainBooking = int.Parse(option.Value);

            foreach (var activeBooking in activeBookings)
            {
                if (activeBooking.PaymentStatus == PaymentStatus.Paid.ToString())
                {
                    var room = rooms.FirstOrDefault(room => room.Id == activeBooking.RoomId);
                    if (room != null && (room.PremierTime - DateTime.UtcNow).TotalSeconds <= timeToRemainBooking)
                    {
                        var userEmail = users.FirstOrDefault(user => user.Id == activeBooking.UserId.ToString()).Email;
                        await dBContext.MessageQueues.AddAsync(new MessageQueue()
                        {
                            Id = Guid.NewGuid(),
                            ContactAddress = userEmail,
                            Type = MessageType.Email.ToString(),
                            Subject = "Booking Remainder",
                            Body = $"You have a booking at {room.PremierTime}",
                            Date = DateTime.UtcNow,
                        });
                    }
                }
            }
        }
    }
}
