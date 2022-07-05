using Movie.Services.Exceptions;

namespace Movie.BO.Services.Exceptions
{
    public class AccessDeniedException : BaseException
    {
        public AccessDeniedException(string message) : base(message)
        {
        }
    }
}
