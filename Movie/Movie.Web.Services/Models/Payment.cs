using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Web.Services.Models
{
    public class Payment
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public DateTime PayemntDT { get; set; }
    }
}
