using ISTIC.Responses.Core;
using System.Net;

namespace ISTIC.Responses.Extensions;

public static class ErrorFactory
{
    public static Error BadRequestError(string description = "Não foi possível realizar a requisição.", Dictionary<string, List<string>> fieldErrors = null)
    {
        Error error = new Error("Bad Request", description, fieldErrors);
        error.SetStatusCode(HttpStatusCode.BadRequest);
        return error;
    }

    public static Error NotFoundError(string resourceName, Dictionary<string, List<string>> fieldErrors = null)
    {
        Error error = new Error("Not Found", $"O recurso {resourceName} não foi encontrado.", fieldErrors);
        error.SetStatusCode(HttpStatusCode.NotFound);
        return error;
    }

    public static Error InternalServerError(string description = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.", Dictionary<string, List<string>> fieldErrors = null)
    {
        Error error = new Error("Internal Server Error", description, fieldErrors);
        error.SetStatusCode(HttpStatusCode.InternalServerError);
        return error;
    }

    public static Error UnauthorizedError(string description = "Você não foi autorizado para acessar este recurso.", Dictionary<string, List<string>> fieldErrors = null)
    {
        Error error = new Error("Unauthorized", description, fieldErrors);
        error.SetStatusCode(HttpStatusCode.Unauthorized);
        return error;
    }

    public static Error ForbiddenError(string description = "Você não tem permissão para acessar este recurso.", Dictionary<string, List<string>> fieldErrors = null)
    {
        Error error = new Error("Forbidden", description, fieldErrors);
        error.SetStatusCode(HttpStatusCode.Forbidden);
        return error;
    }
}
