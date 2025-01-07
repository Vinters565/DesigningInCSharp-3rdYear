using SchedulePlanner.Domain;
using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Application.CalendarEvents.EventAttributes;

public interface IEventAttributesRegistry
{
    IReadOnlyCollection<Type> GetEventAttributeTypes();

    IReadOnlyDictionary<string, Type> GetTypeMapping();

    IReadOnlyCollection<(Type Type, string Name, IReadOnlyCollection<FieldMetadata> Metadata)>
        GetEventAttributesWithMetadata();
}