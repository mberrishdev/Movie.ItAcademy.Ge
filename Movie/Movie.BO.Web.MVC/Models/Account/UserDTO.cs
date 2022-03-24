﻿using System.Collections.Generic;

namespace Movie.BO.Web.MVC.Models.Account
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
