using Movie.Services.Exceptions;

namespace Movie.Web.Services.Exceptions
{
    public class NotExistException : BaseException
    {
        public NotExistException(string message) : base(message)
        {
        }
    }
}
