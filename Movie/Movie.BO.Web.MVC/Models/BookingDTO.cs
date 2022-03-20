using System;

namespace Movie.BO.Web.MVC.Models
{
    public class BookingDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }
        public DateTime BookedDT { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }
    }
}
