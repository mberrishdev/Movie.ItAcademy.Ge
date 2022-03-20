using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.BO.Services.Models.User
{
    public class LogInModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
