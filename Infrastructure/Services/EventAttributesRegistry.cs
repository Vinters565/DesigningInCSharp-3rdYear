using System.Reflection;
using SchedulePlanner.Application.CalendarEvents.EventAttributes;
using SchedulePlanner.Domain;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Infrastructure.Services;

public class EventAttributesRegistry : IEventAttributesRegistry
{
    private readonly List<Type> eventAttributeTypes;
    
    private readonly Dictionary<string, Type> typeMapping;
    
    private readonly List<(Type Type, string Name, IReadOnlyCollection<FieldMetadata> Metadata)>
        eventAttributesWithMetadata;

    public EventAttributesRegistry()
    {
        eventAttributeTypes = LoadEventAttributeTypes();
        typeMapping = LoadTypeMapping();
        eventAttributesWithMetadata = LoadEventAttributesWithMetadata();
    }

    public IReadOnlyCollection<Type> GetEventAttributeTypes() => eventAttributeTypes;

    public IReadOnlyDictionary<string, Type> GetTypeMapping() => typeMapping;

    public IReadOnlyCollection<(Type Type, string Name, IReadOnlyCollection<FieldMetadata> Metadata)> 
        GetEventAttributesWithMetadata() => eventAttributesWithMetadata;
    
    private static List<Type> LoadEventAttributeTypes()
    {
        return Assembly.Load("SchedulePlanner.Domain")
            .GetTypes()
            .Where(t => typeof(IEventAttribute).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();
    }

    private Dictionary<string, Type> LoadTypeMapping()
    {
        return eventAttributeTypes.ToDictionary(t => t.Name, t => t);
    }

    private List<(Type Type, string Name, IReadOnlyCollection<FieldMetadata> Metadata)>
        LoadEventAttributesWithMetadata()
    {
        var result = new List<(Type Type, string Name, IReadOnlyCollection<FieldMetadata> Metadata)>();
        
        foreach (var type in eventAttributeTypes)
        {
            if (Activator.CreateInstance(type, true) is IEventAttribute instance)
            {
                result.Add((type, instance.Name, instance.GetFieldsMetadata()));
            }
        }
        
        return result;
    }
}