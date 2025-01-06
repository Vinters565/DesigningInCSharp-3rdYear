using System.Text.Json;
using System.Text.Json.Serialization;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.JsonConverters
{
    public class EventAttributeIReadOnlyDictionaryConverter: JsonConverter<IReadOnlyDictionary<Type, IEventAttribute>>
    {
        private readonly EventAttributeDictionaryConverter eventAttributeDictionaryConverter;

        public EventAttributeIReadOnlyDictionaryConverter()
        {
            eventAttributeDictionaryConverter = new EventAttributeDictionaryConverter();
        }

        public override IReadOnlyDictionary<Type, IEventAttribute> Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return eventAttributeDictionaryConverter.Read(
                ref reader, typeof(Dictionary<Type, IEventAttribute>), options);
        }

        public override void Write(Utf8JsonWriter writer, IReadOnlyDictionary<Type, IEventAttribute> value, JsonSerializerOptions options)
        {
            eventAttributeDictionaryConverter.Write(writer, value.ToDictionary(), options);
        }
    }
}
