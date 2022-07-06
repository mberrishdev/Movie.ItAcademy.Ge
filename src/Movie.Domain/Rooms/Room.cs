using Movie.Domain.Enums;
using Movie.Domain.Rooms.Commands;
using System;
using System.ComponentModel.DataAnnotations;

namespace Movie.Domain.Rooms
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public DateTime PremierTime { get; set; }
        public int DurationMinutes { get; set; }
        public int RoomUserCapacity { get; set; }
        public int UserCount { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public RoomStatus Status { get; set; }

        public Movies.Movie Movie { get; set; }

        public Room(CreateRoomCommand command)
        {
            PremierTime = command.PremierTime;
            DurationMinutes = command.DurationMinutes;
            RoomUserCapacity = command.RoomUserCapacity;
            UserCount = 0;
            Price = command.Price;
            Currency = command.Currency;
            Status = RoomStatus.New;
        }
    }
}
