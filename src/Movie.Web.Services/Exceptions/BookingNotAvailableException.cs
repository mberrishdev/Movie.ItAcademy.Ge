using Movie.Services.Exceptions;

namespace Movie.Web.Services.Exceptions
{
    public class BookingNotAvailableException : BaseException
    {
        public BookingNotAvailableException(string message) : base(message)
        {
        }
    }
}
