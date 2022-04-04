using Movie.Services.Enums;
using System;

namespace Movie.Web.Services.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime BookedDT { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public BookingStatus Status { get; set; }
    }
}
