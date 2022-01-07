using Entities.ErrorModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ToyWorldSystem.Extensions
{
    public class ErrorHandlingExtension
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingExtension> _logger;

        public ErrorHandlingExtension(RequestDelegate next, ILogger<ErrorHandlingExtension> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingExtension> logger)
        {
            object errors = null;
            switch (ex)
            {
                case ErrorDetails err:
                    logger.LogError(ex, "Internal Sever Error");
                    errors = err.Msg;
                    context.Response.StatusCode = (int)err.StatusCode;
                    break;
                case Exception e:
                    logger.LogError(e, "Sever error");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                var result = JsonSerializer.Serialize(new
                {
                    errors
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
