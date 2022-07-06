using Movie.Domain.Bookings.Commands;
using Movie.Domain.Enums;
using Movie.Domain.POCO;
using System;
using System.ComponentModel.DataAnnotations;

namespace Movie.Domain.Bookings
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime BookedDT { get; set; }
        public string PaymentStatus { get; set; }
        public BookingStatus Status { get; set; }

        public Payment Payment { get; set; }

        private Booking()
        {
        }

        public void ChangeBookingStatus(ChangeBookingStatusCommand command)
        {
            Status = command.Status;
        }
    }
}
