using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ErrorModel
{
    public class ErrorDetails : Exception
    {
        public int StatusCode { get; set; }
        public string Msg { get; set; }

        public ErrorDetails(string msg, int statusCode)
        {
            Msg = msg;
            StatusCode = statusCode;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
