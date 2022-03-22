using Movie.Data;
using Movie.Services.Enums;
using Movie.Worker.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class BookingService : IBookingService
    {
        public readonly IBookingRepository _bookingRepository;
        public readonly IRoomRepository _roomRepository;


        public BookingService(IBookingRepository bookingRepository,IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;

        }

        public async void CheckAndCancellBookings()
        {
            var activeBookings = await _bookingRepository.GetAlActiveBookingsAsync();
            var rooms = await _roomRepository.GetAllRoomsAsync();
            foreach (var activeBooking in activeBookings)
            {
                if (activeBooking.PaymentStatus == PaymentStatus.Unpaid.ToString())
                {
                    var room = rooms.FirstOrDefault(room => room.Id == activeBooking.RoomId);
                    if (room != null && (room.PremierTime - DateTime.UtcNow).TotalSeconds <= 3600)
                        await _bookingRepository.ChangeBookingStatusAsync(activeBooking.Id, BookingStatus.CancelledByWorker.ToString());
                }
            }
        }
    }
}
