using Bank.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Bank.Application.Exceptions
{
    public class BranchException : Exception
    {

        public int StatusCode { get; }
        
        
        public BranchException() { }

        public BranchException(string message)
            : base(message) { }

        public BranchException(string message, int statusCode = StatusCodes.Status400BadRequest)
           : base(message)
        {
            StatusCode = statusCode;
        }

        //public BranchException(string message, Exception innerException, int statusCode = StatusCodes.Status400BadRequest)
        //    : base(message, innerException)
        //{
        //    StatusCode = statusCode;
        //}

    }
}
