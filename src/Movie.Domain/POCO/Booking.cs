using System;
using System.ComponentModel.DataAnnotations;

namespace Movie.Domain.POCO
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime BookedDT { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }

        public Payment Payment { get; set; }
    }
}
