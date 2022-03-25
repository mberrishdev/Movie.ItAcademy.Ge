using System;
using System.ComponentModel.DataAnnotations;

namespace Movie.BO.Web.MVC.Models.Movie
{
    public class MovieDTO
    {
        [Required]
        public Guid RoomId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gener { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        [Display(Name = "IMDB Reiting?")]
        public decimal IMDBReiting { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string BannerUrl { get; set; }
    }
}
