using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GlobalExceptionMiddleware
{
    public class ExceptionErrors
    {
        public ExceptionErrors(int ExceptionErrorCode, string ExceptionErrorMessage, string ExceptionErrorDetails)
        {
            ErrorCode = ExceptionErrorCode;
            ErrorMessage = ExceptionErrorMessage;
            ErrorDetails = ExceptionErrorDetails;

        }
        public int ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }
        public string ErrorDetails { get; private set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
