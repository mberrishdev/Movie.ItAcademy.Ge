using Movie.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Web.Services.Exceptions
{
    public class BookingNotAvailableException : BaseException
    {
        public BookingNotAvailableException(string message) : base(message)
        {
        }
    }
}
