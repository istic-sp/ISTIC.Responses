using System.Net;
using ISTIC.Responses.Interfaces;

namespace ISTIC.Responses.Core;

public class Response : IResponse
{
    public Error Error { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static Response Success(HttpStatusCode statusCode = HttpStatusCode.OK) => new() { StatusCode = statusCode };
    public static Response ErrorHandle(string name, string description, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => Throw(new Error(name, description), statusCode);

    private static Response Throw(Error error, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => new()
    {
        Error = error,
        StatusCode = error.GetStatusCode().HasValue ? error.GetStatusCode().Value : statusCode
    };

    public static implicit operator Response(Error error) => Throw(error);
}
