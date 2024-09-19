﻿using ISTIC.Responses.Core;

namespace ISTIC.Responses.Extensions;

public static class ErrorFactory
{
    public static Error BadRequestError(string description = "Não foi possível realizar a requisição.", Dictionary<string, List<string>> fieldErrors = null)
        => new Error("Bad Request", description, fieldErrors);

    public static Error NotFoundError(string resourceName, Dictionary<string, List<string>> fieldErrors = null)
        => new Error("Not Found", $"O recurso {resourceName} não foi encontrado.", fieldErrors);

    public static Error InternalServerError(string description = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.", Dictionary<string, List<string>> fieldErrors = null)
        => new Error("Internal Server Error", description, fieldErrors);

    public static Error UnauthorizedError(string description = "Você não foi autorizado para acessar este recurso.", Dictionary<string, List<string>> fieldErrors = null)
        => new Error("Unauthorized", description, fieldErrors);

    public static Error ForbiddenError(string description = "Você não tem permissão para acessar este recurso.", Dictionary<string, List<string>> fieldErrors = null)
        => new Error("Forbidden", description, fieldErrors);
}