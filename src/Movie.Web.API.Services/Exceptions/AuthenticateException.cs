using Movie.Services.Exceptions;

namespace Movie.Web.API.Services.Exceptions
{
    public class AuthenticateException : BaseException
    {
        public AuthenticateException(string message) : base(message)
        {
        }
    }
}
