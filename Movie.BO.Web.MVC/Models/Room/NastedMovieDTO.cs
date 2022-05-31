using System.ComponentModel.DataAnnotations;

namespace Movie.BO.Web.MVC.Models.Room
{
    public class NastedMovieDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gener { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public decimal IMDBReiting { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string BannerUrl { get; set; }
    }
}
