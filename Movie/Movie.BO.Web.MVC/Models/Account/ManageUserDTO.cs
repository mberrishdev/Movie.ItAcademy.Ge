using System.ComponentModel.DataAnnotations;

namespace Movie.BO.Web.MVC.Models.Account
{
    public class ManageUserDTO
    {
        [Required]
        public string RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public bool Selected { get; set; }
    }
}
