using System.Net;

namespace Stick.Core.Responses.Interfaces;

public interface IBaseResponse
{
    public HttpStatusCode StatusCode { get; set; }
}
