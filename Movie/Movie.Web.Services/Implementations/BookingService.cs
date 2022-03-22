using Mapster;
using Movie.Data;
using Movie.Services.Enums;
using Movie.Web.Services.Abstractions;
using Movie.Web.Services.Exceptions;
using Movie.Web.Services.Models;
using System;
using System.Threading.Tasks;

namespace Movie.Web.Services.Implementations
{
    public class BookingService : IBookingService
    {
        public readonly IBookingRepository _bookingRepository;
        public readonly IRoomService _roomService;


        public BookingService(IBookingRepository bookingRepository, IRoomService roomService)
        {
            _bookingRepository = bookingRepository;
            _roomService = roomService;
        }

        public async Task BookRoomAsync(Guid roomId, Guid userId)
        {
            Room room = await _roomService.GetRoomAsync(roomId);

            int secondsInOneHour = 3600;

            double timeDifferenceInSeconds = (room.PremierTime - DateTime.UtcNow).TotalSeconds;

            if (timeDifferenceInSeconds <= secondsInOneHour)
                throw new BookingNotAvailableException("Booking not available");

            if (room.RoomUserCapacity <= room.UserCount)
                throw new RoomIsFullException($"Room Id:{roomId} is full");

            //create Booking Model
            Booking bookingModel = new Booking()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                RoomId = roomId,
                BookedDT = DateTime.UtcNow,
                PaymentStatus = PaymentStatus.Unpaid,
                Status = BookingStatus.Active
            };

            await _bookingRepository.BookRoomAsync(bookingModel.Adapt<Domain.POCO.Booking>());

            //Increase room user Count
            await _roomService.IncreaseUserCountAsync(roomId);
        }

        public async Task<bool> IsExistAsync(Guid bookingId)
        {
            return await _bookingRepository.IsExistAsync(bookingId);
        }
    }
}
