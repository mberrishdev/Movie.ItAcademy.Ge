using Movie.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.BO.Services.Exceptions
{
    public class AccessDeniedException : BaseException
    {
        public AccessDeniedException(string message) : base(message)
        {
        }
    }
}
