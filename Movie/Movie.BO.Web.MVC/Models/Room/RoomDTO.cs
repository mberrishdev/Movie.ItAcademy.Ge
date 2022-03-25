using System;
using System.ComponentModel.DataAnnotations;

namespace Movie.BO.Web.MVC.Models.Room
{
    public class RoomDTO
    {
        [Required]
        [Display(Name = "Premier Time?")]
        public DateTime PremierTime { get; set; }

        [Required]
        [Display(Name = "Room User Capacity?")]
        public int RoomUserCapacity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Currency { get; set; }
    }
}
