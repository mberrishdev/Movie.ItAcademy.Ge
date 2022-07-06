using System;

namespace Movie.Domain.Movies.Commands
{
    public class CreateMovieCommand
    {
        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public string Gener { get; set; }
        public string Director { get; set; }
        public decimal IMDBReiting { get; set; }
        public string Description { get; set; }
        public string BannerUrl { get; set; }
    }
}
