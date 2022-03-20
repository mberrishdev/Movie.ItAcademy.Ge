using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.POCO
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }
        public DateTime BookedDT { get; set; }
        public Payment Payment { get; set; }
    }
}
