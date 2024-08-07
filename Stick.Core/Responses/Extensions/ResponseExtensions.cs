using Stick.Core.Responses;
using System.Net;

namespace Stick.Core.Responses.Extensions;

public static class ResponseExtensions
{
    public static BaseResponse<T> WithSuccessStatusCode<T>(this T result, HttpStatusCode statusCode) where T : class
    {
        return new BaseResponse<T>(result, statusCode);
    }

    public static BaseResponse<T> WithErrorStatusCode<T>(this ErrorResponse errorResponse, HttpStatusCode statusCode)
    {
        return new BaseResponse<T>(errorResponse, statusCode);
    }

    public static Response WithErrorStatusCode(this ErrorResponse errorResponse, HttpStatusCode statusCode)
    {
        return Response.ErrorHandle(errorResponse.Name, errorResponse.Description, statusCode);
    }
}
