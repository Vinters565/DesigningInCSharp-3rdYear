using System.Reflection;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.EventAttributes;

public static class EventAttributesRegistry
{
    private static readonly List<Type> eventAttributeTypes = LoadEventAttributeTypes();
    
    private static readonly Dictionary<string, Type> typeMapping = LoadTypeMapping();
    
    private static readonly List<(Type Type, string Name, IReadOnlyCollection<FieldMetadata> Metadata)>
        eventAttributesWithMetadata = LoadEventAttributesWithMetadata();

    public static IReadOnlyCollection<Type> GetEventAttributeTypes() => eventAttributeTypes;

    public static IReadOnlyDictionary<string, Type> GetTypeMapping() => typeMapping;

    public static IReadOnlyCollection<(Type Type, string Description, IReadOnlyCollection<FieldMetadata> Metadata)> 
        GetEventAttributesWithMetadata() => eventAttributesWithMetadata;
    
    private static List<Type> LoadEventAttributeTypes()
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IEventAttribute).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();
    }

    private static Dictionary<string, Type> LoadTypeMapping()
    {
        return eventAttributeTypes.ToDictionary(t => t.Name, t => t);
    }

    private static List<(Type Type, string Description, IReadOnlyCollection<FieldMetadata> Metadata)>
        LoadEventAttributesWithMetadata()
    {
        var result = new List<(Type Type, string Name, IReadOnlyCollection<FieldMetadata> Metadata)>();
        
        foreach (var type in eventAttributeTypes)
        {
            if (Activator.CreateInstance(type, true) is IEventAttribute instance)
            {
                result.Add((type, instance.GetDescription(), instance.GetFieldsMetadata()));
            }
        }
        
        return result;
    }
}