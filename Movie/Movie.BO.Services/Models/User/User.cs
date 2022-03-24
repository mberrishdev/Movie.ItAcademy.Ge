using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Movie.BO.Services.Models.User
{
    public class User : IdentityUser
    {
        public IEnumerable<string> Roles { get; set; }
    }
}
