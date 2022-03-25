using System;

namespace Movie.Web.API.Models
{
    public class RoomWithMovieViewModel
    {
        public Guid Id { get; set; }
        public DateTime PremierTime { get; set; }
        public int RoomUserCapacity { get; set; }
        public int UserCount { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }

        public NastedMovieViewModel Movie { get; set; }
    }
}
