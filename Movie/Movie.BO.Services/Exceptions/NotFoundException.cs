using Movie.Services.Exceptions;

namespace Movie.BO.Services.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
