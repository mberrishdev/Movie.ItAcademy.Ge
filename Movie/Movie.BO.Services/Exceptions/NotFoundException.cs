using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.BO.Services.Exceptions
{
    public class NotFoundException:BaseException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
