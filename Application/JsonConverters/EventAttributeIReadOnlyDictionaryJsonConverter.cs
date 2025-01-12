using System.Text.Json;
using System.Text.Json.Serialization;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.JsonConverters
{
    public class EventAttributeIReadOnlyDictionaryJsonConverter: JsonConverter<IReadOnlyDictionary<Type, IEventAttribute>>
    {
        public override IReadOnlyDictionary<Type, IEventAttribute> Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<Dictionary<Type, IEventAttribute>>(ref reader, options)
                ?? throw new JsonException("Cannot deserialize IReadOnlyDictionary<Type, IEventAttribute>");
        }

        public override void Write(Utf8JsonWriter writer, IReadOnlyDictionary<Type, IEventAttribute> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.ToDictionary(), options);
        }
    }
}
