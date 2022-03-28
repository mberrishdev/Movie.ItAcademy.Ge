using Microsoft.AspNetCore.Identity;
using Movie.Data;
using Movie.Domain.POCO;
using Movie.Services.Abstractions;
using Movie.Services.Enums;
using Movie.Worker.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IAspNetUserRepository _aspNetUserRepository;
        public readonly IBookingRepository _bookingRepository;
        public readonly IRoomRepository _roomRepository;
        public readonly IMessageQueueRepository _messageQueueRepository;
        public readonly IServerOptionService _serverOptionService;


        public EmailService(IAspNetUserRepository aspNetUserRepository, IBookingRepository bookingRepository,
            IRoomRepository roomRepository,
            IMessageQueueRepository messageQueueRepository,
            IServerOptionService serverOptionService)
        {
            _aspNetUserRepository = aspNetUserRepository;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _messageQueueRepository = messageQueueRepository;
            _serverOptionService = serverOptionService;
        }


        public async Task CheckAndSendEmail()
        {
            var activeBookings = await _bookingRepository.GetAlActiveBookingsAsync();
            var users = await _aspNetUserRepository.GetUsersAsync();
            var rooms = await _roomRepository.GetAllRoomsAsync();

            var option = await _serverOptionService.GetOptionAsync("move.booking.time.to.remainder.email.sec");
            var timeToRemainBooking = int.Parse(option.Value);

            foreach (var activeBooking in activeBookings)
            {
                if (activeBooking.PaymentStatus == PaymentStatus.Paid.ToString())
                {
                    var room = rooms.FirstOrDefault(room => room.Id == activeBooking.RoomId);
                    if (room != null && (room.PremierTime - DateTime.UtcNow).TotalSeconds <= timeToRemainBooking)
                    {
                        var userEmail = users.FirstOrDefault(user => user.Id == activeBooking.UserId.ToString()).Email;
                        await _messageQueueRepository.AddIntoQueueAsync(new MessageQueue()
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
