using System.Net;
using ISTIC.Responses.Core;

namespace ISTIC.Responses.Extensions;

public static class ResponseExtensions
{
    public static ResponseOf<T> WithSuccessStatusCode<T>(this T result, HttpStatusCode statusCode) where T : class
    {
        return new ResponseOf<T>(result, statusCode);
    }

    public static ResponseOf<T> WithErrorStatusCode<T>(this Error errorResponse, HttpStatusCode statusCode)
    {
        return new ResponseOf<T>(errorResponse, statusCode);
    }

    public static Response WithErrorStatusCode(this Error errorResponse, HttpStatusCode statusCode)
    {
        return Response.ErrorHandle(errorResponse.Name, errorResponse.Description, statusCode);
    }
}
