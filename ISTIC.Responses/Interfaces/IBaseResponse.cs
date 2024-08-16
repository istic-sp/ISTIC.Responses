using System.Net;

namespace ISTIC.Responses.Interfaces;

public interface IBaseResponse
{
    public HttpStatusCode StatusCode { get; set; }
}
