using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.BO.Services.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }
        public DateTime BookedDT { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }
    }
}
