using System;

namespace Movie.Domain.Rooms.Commands
{
    public class CreateRoomCommand
    {
        public DateTime PremierTime { get; set; }
        public int DurationMinutes { get; set; }
        public int RoomUserCapacity { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
