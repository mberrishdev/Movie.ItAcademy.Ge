using Movie.Services.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Web.Services.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public DateTime PremierTime { get; set; }
        public int DurationMinutes { get; set; }
        public int RoomUserCapacity { get; set; }
        public int UserCount { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public RoomStatus Status { get; set; }
        
        public Movie Movie { get; set; }
    }
}
