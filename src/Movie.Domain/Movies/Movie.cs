using Movie.Domain.Movie.Commands;
using Movie.Domain.POCO;
using System;

namespace Movie.Domain.Movies
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

        private Movie() { }

        public Movie(CreateMovieCommand command)
        {
            Name = command.Name;
            Gener = command.Gener;
            Director = command.Director;
            IMDBReiting = command.IMDBReiting;
            Description = command.Description;
            BannerUrl = command.BannerUrl;
        }
    }
}
