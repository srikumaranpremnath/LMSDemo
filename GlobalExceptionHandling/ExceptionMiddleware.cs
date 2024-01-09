using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace GlobalExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception exception)
            {
                string Message;

                var ExceptionType = exception.GetType();
                ExceptionErrors exceptionErrors;
                _logger.LogError($"Something went wrong: {exception}");
                HttpStatusCode statusCode;
                if (ExceptionType == typeof(UnauthorizedAccessException))
                {
                    statusCode = HttpStatusCode.Forbidden;
                    Message = "You are Unauthorized";

                }
                else
                {
                    statusCode = HttpStatusCode.InternalServerError;
                    Message = "Unknown Error, Contact ADMIN";

                }
                if (_env.IsDevelopment())
                {
                    exceptionErrors = new ExceptionErrors
                    (
                    Convert.ToInt32(statusCode), exception.Message, exception.StackTrace.ToString()
                    );
                }
                else
                {
                    exceptionErrors = new ExceptionErrors(Convert.ToInt32(statusCode), Message, null);
                }
                context.Response.StatusCode = Convert.ToInt32(statusCode);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(exceptionErrors.ToString()); ;

            }
        }
    }
}
