using System.Net;
using Stick.Core.Responses.Interfaces;

namespace Stick.Core.Responses;

public class Response : IBaseResponse
{
    public ErrorResponse Error { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static Response Success(HttpStatusCode statusCode = HttpStatusCode.OK) => new() { StatusCode = statusCode };
    public static Response ErrorHandle(string name, string description, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => Throw(new ErrorResponse(name, description), statusCode);

    private static Response Throw(ErrorResponse errorResponse, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => new()
    {
        Error = errorResponse,
        StatusCode = statusCode
    };

    public static implicit operator Response(ErrorResponse errorResponse) => Throw(errorResponse);
}
