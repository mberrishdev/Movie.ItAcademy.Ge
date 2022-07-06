using Movie.Domain.Enums;
using System;

namespace Movie.Domain.Bookings.Commands
{
    public class ChangeBookingStatusCommand
    {
        public Guid BookId { get; set; }
        public BookingStatus Status { get; set; }
    }
}
