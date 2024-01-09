using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerResponse
{
    public class ControllerResponses
    {
        public static ActionResult ControllerResponses<TModel>(
          this BaseController controller,
          IBaseResponse<TModel> response,
          Func<object, ActionResult> onSuccess,
          string entityName = null)
          where TModel : class
        {
            if (response.Errors.Count > 0)
            {
                response.Errors.ToList().ForEach(x => x.Location = controller.HttpContext.Request.Path);
                var error = response.Errors.FirstOrDefault();
                switch (error.Category)
                {
                    case ErrorCategory.Error:
                        return controller.BadRequest(
                            new RestErrorResponse(StatusCodes.Status400BadRequest.ToString(), "Bad Request", response.Errors.ToList()));
                    case ErrorCategory.NotFound:
                        return controller.NotFound(
                            new RestErrorResponse(StatusCodes.Status404NotFound.ToString(), "Not Found", response.Errors.ToList()));
                    case ErrorCategory.Duplicate:
                        return controller.Conflict(
                            new RestErrorResponse(StatusCodes.Status409Conflict.ToString(), "Duplicate data", response.Errors.ToList()));
                    case ErrorCategory.BusinessRuleFailed:
                        return controller.UnprocessableEntity(
                            new RestErrorResponse(StatusCodes.Status422UnprocessableEntity.ToString(), "Unprocessable Entity", response.Errors.ToList()));
                    default:
                        return controller.BadRequest(
                       new RestErrorResponse(StatusCodes.Status400BadRequest.ToString(), "Bad Request", response.Errors.ToList()));
                }
            }
          
            else
                return onSuccess(response.Result);
        }
    }
}
}
