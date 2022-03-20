using System;
using System.ComponentModel.DataAnnotations;

namespace Movie.BO.Web.MVC.Models.Movie
{
    public class MovieCreateModel
    {
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
        [Display(Name = "Premier Time?")]
        public DateTime PremierTime { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(5, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Currency { get; set; }
        [Required]
        public string BannerUrl { get; set; }
    }
}
