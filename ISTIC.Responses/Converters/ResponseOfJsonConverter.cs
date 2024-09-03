using System.Text.Json.Serialization;
using System.Text.Json;
using ISTIC.Responses.Core;

namespace ISTIC.Responses.Converters;

public class ResponseOfJsonConverter<T> : JsonConverter<ResponseOf<T>>
{
    public override ResponseOf<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ResponseOf<T> value, JsonSerializerOptions options)
    {
        if (value.Error != null)
        {
            JsonSerializer.Serialize(writer, value.Error, options);
        }
        else
        {
            JsonSerializer.Serialize(writer, value.Result, options);
        }
    }
}
