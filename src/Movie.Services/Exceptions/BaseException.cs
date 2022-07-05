using Microsoft.AspNetCore.Mvc;
using System;

namespace Movie.Services.Exceptions
{
    public class BaseException : Exception
    {
        public ApiError ApiError { get; set; }
        private string ErrorMessage { get; set; }

        protected BaseException(string message)
            : base(message) => ErrorMessage = message;

        public BaseException(string message, Exception exception)
            : base(message, exception) => ErrorMessage = message;

        public BaseException AddApiError(int errorCode)
        {
            ApiError = new ApiError
            {
                Status = errorCode,
                Detail = ErrorMessage
            };
            return this;
        }
    }

    public class ApiError : ProblemDetails
    {
    }
}
