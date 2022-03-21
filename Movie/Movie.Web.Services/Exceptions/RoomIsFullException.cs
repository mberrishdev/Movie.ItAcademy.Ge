using Movie.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Web.Services.Exceptions
{
    public class RoomIsFullException : BaseException
    {
        public RoomIsFullException(string message) : base(message)
        {
        }
    }
}
