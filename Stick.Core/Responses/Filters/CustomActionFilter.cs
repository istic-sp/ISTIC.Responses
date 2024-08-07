using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Stick.Core.Responses.Interfaces;

namespace Stick.Core.Responses.Filters;

public class CustomActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
            if (objectResult.Value is IBaseResponse baseResponse)
                context.HttpContext.Response.StatusCode = (int)baseResponse.StatusCode;
    }
}
