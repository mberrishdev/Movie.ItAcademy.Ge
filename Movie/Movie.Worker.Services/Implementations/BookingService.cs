using Movie.Data;
using Movie.Services.Abstractions;
using Movie.Services.Enums;
using Movie.Worker.Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class BookingService : IBookingService
    {
        public readonly IBookingRepository _bookingRepository;
        public readonly IRoomRepository _roomRepository;
        private readonly IServerOptionService _serverOptionService;

        public BookingService(IBookingRepository bookingRepository,
            IRoomRepository roomRepository,
            IServerOptionService serverOptionService)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _serverOptionService = serverOptionService;

        }

        public async Task CheckAndCancellBookings()
        {
            var activeBookings = await _bookingRepository.GetAlActiveBookingsAsync();
            var rooms = await _roomRepository.GetAllRoomsAsync();

            var option = await _serverOptionService.GetOptionAsync("move.booking.time.to.cancel.sec");
            var timeToCancelBooking = int.Parse(option.Value);

            foreach (var activeBooking in activeBookings)
            {
                if (activeBooking.PaymentStatus == PaymentStatus.Unpaid.ToString())
                {
                    var room = rooms.FirstOrDefault(room => room.Id == activeBooking.RoomId);
                    if (room != null && (room.PremierTime - DateTime.UtcNow).TotalSeconds <= timeToCancelBooking)
                        await _bookingRepository.ChangeBookingStatusAsync(activeBooking.Id, BookingStatus.CancelledByWorker.ToString());
                }
            }
        }
    }
}
