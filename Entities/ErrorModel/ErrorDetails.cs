using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

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
