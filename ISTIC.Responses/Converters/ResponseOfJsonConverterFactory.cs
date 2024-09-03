using System.Text.Json.Serialization;
using System.Text.Json;
using ISTIC.Responses.Core;

namespace ISTIC.Responses.Converters;

public class ResponseOfJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsGenericType &&
               typeToConvert.GetGenericTypeDefinition() == typeof(ResponseOf<>);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var genericArgument = typeToConvert.GetGenericArguments()[0];
        var converterType = typeof(ResponseOfJsonConverter<>).MakeGenericType(genericArgument);

        return (JsonConverter)Activator.CreateInstance(converterType);
    }
}
