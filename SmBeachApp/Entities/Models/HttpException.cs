using System;
using System.Net;

namespace Mch.MainManagerSrv.Data.Models
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public object[] Arguments { get; set; }

        public HttpException(HttpStatusCode statusCode, string message, params object[] arguments) : base(message)
        {
            StatusCode = statusCode;
            Arguments = arguments;
        }
    }
}
