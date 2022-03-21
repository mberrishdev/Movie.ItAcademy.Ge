using Movie.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Web.Services.Exceptions
{
    public class NotExistException : BaseException
    {
        public NotExistException(string message) : base(message)
        {
        }
    }
}
