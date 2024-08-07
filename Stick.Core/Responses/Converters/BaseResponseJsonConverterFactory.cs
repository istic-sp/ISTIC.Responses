using System.Text.Json.Serialization;
using System.Text.Json;

namespace Stick.Core.Responses.Converters;

public class BaseResponseJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsGenericType &&
               typeToConvert.GetGenericTypeDefinition() == typeof(BaseResponse<>);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var genericArgument = typeToConvert.GetGenericArguments()[0];
        var converterType = typeof(BaseResponseJsonConverter<>).MakeGenericType(genericArgument);

        return (JsonConverter)Activator.CreateInstance(converterType);
    }
}
