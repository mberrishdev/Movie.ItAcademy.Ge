using System.ComponentModel.DataAnnotations;

namespace Movie.BO.Web.MVC.Models.Account
{
    public class LogInDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
