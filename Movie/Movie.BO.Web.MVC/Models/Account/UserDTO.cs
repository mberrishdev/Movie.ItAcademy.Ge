using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie.BO.Web.MVC.Models.Account
{
    public class UserDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public IEnumerable<string> Roles { get; set; }
    }
}
