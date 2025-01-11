using System.Text.Json;
using System.Text.Json.Serialization;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.JsonConverters
{
    public class EventAttributeDictionaryJsonConverter : JsonConverter<Dictionary<Type, IEventAttribute>>
    {
        public override Dictionary<Type, IEventAttribute> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var result = new Dictionary<Type, IEventAttribute>();

            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            if (root.ValueKind != JsonValueKind.Object)
            {
                throw new JsonException("Expected JSON object for EventAttribute dictionary.");
            }

            foreach (var property in root.EnumerateObject())
            {
                var typeName = property.Name;
                var rawJson = property.Value.GetRawText();

                if (EventAttributesRegistry.GetTypeMapping().TryGetValue(typeName, out var targetType))
                {
                    var attribute = JsonSerializer.Deserialize(rawJson, targetType, options) as IEventAttribute
                                    ?? throw new Exception(
                                        $"EventAttribute deserialization exception for type: {typeName}");
                    result[targetType] = attribute;
                }
                else
                {
                    throw new JsonException($"Unknown type: {typeName}");
                }
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<Type, IEventAttribute> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (var kvp in value)
            {
                var typeName = kvp.Key.Name;
                writer.WritePropertyName(typeName);
                JsonSerializer.Serialize(writer, kvp.Value, kvp.Value.GetType(), options);
            }

            writer.WriteEndObject();
        }
    }
}
