using System.Net;
using ISTIC.Responses.Interfaces;

namespace ISTIC.Responses.Core;

public class ResponseOf<T> : IResponse
{
    public Error Error { get; set; }
    public T Result { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public ResponseOf() { }

    public ResponseOf(T result, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        Result = result;
        StatusCode = statusCode;
    }

    public ResponseOf(Error error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        Error = error;
        StatusCode = error.GetStatusCode().HasValue ? error.GetStatusCode().Value : statusCode;
    }


    public static implicit operator ResponseOf<T>(Response response) => new ResponseOf<T>
    {
        Error = response.Error,
        StatusCode = response.Error != null ? response.Error.GetStatusCode().HasValue ? response.Error.GetStatusCode().Value : response.StatusCode : response.StatusCode
    };

    public static implicit operator ResponseOf<T>(T data) => new ResponseOf<T>(data);

    public static implicit operator ResponseOf<T>(Error error) => new ResponseOf<T>(error);
}