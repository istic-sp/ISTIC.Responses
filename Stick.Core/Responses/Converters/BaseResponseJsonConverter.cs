using System.Text.Json.Serialization;
using System.Text.Json;

namespace Stick.Core.Responses.Converters;

public class BaseResponseJsonConverter<T> : JsonConverter<BaseResponse<T>>
{
    public override BaseResponse<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, BaseResponse<T> value, JsonSerializerOptions options)
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
