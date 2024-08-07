using System.Net;
using Stick.Core.Responses.Interfaces;

namespace Stick.Core.Responses;

public class BaseResponse<T> : IBaseResponse
{
    public ErrorResponse Error { get; set; }
    public T Result { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public BaseResponse() { }

    public BaseResponse(T result, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        Result = result;
        StatusCode = statusCode;
    }

    public BaseResponse(ErrorResponse error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        Error = error;
        StatusCode = statusCode;
    }


    public static implicit operator BaseResponse<T>(T data) => new BaseResponse<T>(data);

    public static implicit operator BaseResponse<T>(ErrorResponse errorResponse) => new BaseResponse<T>(errorResponse);
}