﻿using System;

namespace Movie.Domain.POCO
{
    public class Payment
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public DateTime PayemntDT { get; set; }

        public Booking Booking { get; set; }
    }
}
