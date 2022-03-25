using System;

namespace Movie.BO.Web.MVC.Models.Room
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }
        public DateTime PremierTime { get; set; }
        public int RoomUserCapacity { get; set; }
        public int UserCount { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
    }
}
