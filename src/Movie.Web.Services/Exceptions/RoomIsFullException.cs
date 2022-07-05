using Movie.Services.Exceptions;

namespace Movie.Web.Services.Exceptions
{
    public class RoomIsFullException : BaseException
    {
        public RoomIsFullException(string message) : base(message)
        {
        }
    }
}
