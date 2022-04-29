using Newtonsoft.Json;
using System;
using System.Net;

namespace Entities.ErrorModel
{
    public class ErrorDetails : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public Object Error { get; }

        public ErrorDetails(HttpStatusCode code, object error = null)
        {
            Error = error;
            StatusCode = code;
        }

        public ErrorDetails()
        {
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
