using System;

namespace Movie.Domain.POCO
{
    public class Movie
    {
        public Guid RoomId { get; set; }

        public string Name { get; set; }
        public string Gener { get; set; }
        public string Director { get; set; }
        public decimal IMDBReiting { get; set; }
        public string Description { get; set; }
        public string BannerUrl { get; set; }

        public Room Room { get; set; }
    }
}
