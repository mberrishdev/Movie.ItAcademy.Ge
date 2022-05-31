using System;
using System.ComponentModel.DataAnnotations;

namespace Movie.BO.Web.MVC.Models.Room
{
    public class RoomWithMovieDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime PremierTime { get; set; }
        [Required]
        public int DurationMinutes { get; set; }
        [Required]
        public int RoomUserCapacity { get; set; }
        [Required]
        public int UserCount { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public string Status { get; set; }

        [Required]
        public NastedMovieDTO Movie { get; set; }
    }
}
