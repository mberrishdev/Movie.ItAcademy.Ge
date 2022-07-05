using Microsoft.AspNetCore.Identity;
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
        private IBaseRepository _repository;

        public async Task CheckAndSendEmail(MovieDBContext dBContext)
        {
            _repository = new BaseRepository(dBContext);

            System.Collections.Generic.List<Booking> activeBookings = await _repository
                .WhereAsync<Booking>(booking => booking.Status == "Active");
            System.Collections.Generic.List<IdentityUser> users = await _repository
                .GetAllAsync<IdentityUser>();
            System.Collections.Generic.List<Room> rooms = await _repository
                .GetAllAsync<Room>();

            ServerOption option = await _repository
                .FirstOrDefaultAsync<ServerOption>(op => op.Key == "move.booking.time.to.remainder.email.sec");

            int timeToRemainBooking = int.Parse(option.Value);

            foreach (Booking activeBooking in activeBookings)
            {
                if (activeBooking.PaymentStatus == PaymentStatus.Paid.ToString())
                {
                    Room room = rooms.FirstOrDefault(room => room.Id == activeBooking.RoomId);
                    if (room != null && (room.PremierTime - DateTime.UtcNow).TotalSeconds <= timeToRemainBooking)
                    {
                        string userEmail = users.FirstOrDefault(user => user.Id == activeBooking.UserId.ToString()).Email;
                        await _repository.AddAsync<MessageQueue>(new MessageQueue()
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
