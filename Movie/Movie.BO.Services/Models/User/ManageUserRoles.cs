using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.BO.Services.Models.User
{
    public class ManageUserRoles
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }  
        public bool Selected { get; set; }
    }
}
