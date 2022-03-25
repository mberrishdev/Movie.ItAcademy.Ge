using Movie.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Web.API.Services.Exceptions
{
    public class AuthenticateException : BaseException
    {
        public AuthenticateException(string message) : base(message)
        {
        }
    }
}
