using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ISTIC.Responses.Interfaces;

namespace ISTIC.Responses.Filters;

public class CustomActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
            if (objectResult.Value is IResponse response)
                context.HttpContext.Response.StatusCode = (int)response.StatusCode;
    }
}
