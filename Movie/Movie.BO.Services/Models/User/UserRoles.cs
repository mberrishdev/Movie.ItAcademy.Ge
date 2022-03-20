using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.BO.Services.Models.User
{
    public class UserRoles
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
