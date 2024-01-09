using Microsoft.AspNetCore.Mvc;
using Responses;
using System;

namespace LibraryManagementSystem.Controllers
{
    public static class ControllerResponseProvider
    {
        public static ActionResult ControllerResponse<T>(this BaseAPIController controller,IBaseResponse<T> response,
          Func<object, ActionResult> onSuccess) where T : class
        {
           
            if (response.Errors.Count > 0)
                return controller.BadRequest(response.Errors);

            return onSuccess(response.Result);
        }

        
    }
}
