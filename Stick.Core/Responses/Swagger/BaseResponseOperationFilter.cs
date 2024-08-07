using Microsoft.OpenApi.Models;
using Stick.Core.Responses;
using Stick.Core.Responses.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Stick.Core.Responses.Swagger;

public class BaseResponseOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var returnType = context.MethodInfo.ReturnType;
        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
        {
            returnType = returnType.GenericTypeArguments[0];
        }

        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(BaseResponse<>))
        {
            var successReturnType = returnType.GenericTypeArguments[0];

            if (successReturnType.IsGenericType && successReturnType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                return;

            var successMediaType = new OpenApiMediaType
            {
                Schema = new OpenApiSchema
                {
                    Reference = new OpenApiReference
                    {
                        Id = successReturnType.GetSchemaId(),
                        Type = ReferenceType.Schema
                    }
                }
            };

            var errorMediaType = new OpenApiMediaType
            {
                Schema = new OpenApiSchema
                {
                    Reference = new OpenApiReference
                    {
                        Id = typeof(ErrorResponse).GetSchemaId(),
                        Type = ReferenceType.Schema
                    }
                }
            };

            operation.Responses["200"] = new OpenApiResponse
            {
                Content =
                    {
                        { "application/json",  successMediaType},
                    }
            };

            operation.Responses["400"] = new OpenApiResponse
            {
                Content =
                    {
                        { "application/json", errorMediaType }
                    }
            };
        }
    }
}