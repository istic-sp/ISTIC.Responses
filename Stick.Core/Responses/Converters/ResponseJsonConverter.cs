using System.Text.Json.Serialization;
using System.Text.Json;

namespace Stick.Core.Responses.Converters;

public class ResponseJsonConverter : JsonConverter<Response>
{
    public override Response Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Response value, JsonSerializerOptions options)
    {
        if (value.Error != null)
            JsonSerializer.Serialize(writer, value.Error, options);
    }
}
