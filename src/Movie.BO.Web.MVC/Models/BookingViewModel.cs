using System;

namespace Movie.BO.Web.MVC.Models
{
    public class BookingViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime BookedDT { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }
    }
}
