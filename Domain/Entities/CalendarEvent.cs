using SchedulePlanner.Domain.Common;

namespace SchedulePlanner.Domain.Entities;

public class CalendarEvent : Entity<Guid>
{
    private readonly Dictionary<Type, CalendarEventAttribute> attributes = new();
    
    public CalendarEvent() : base(Guid.NewGuid())
    {
        
    }

    public CalendarEvent AddAttribute<T>(T newAttribute) where T : CalendarEventAttribute
    {
        attributes.Add(typeof(T), newAttribute);
        return this;
    }

    public T GetAttribute<T>() where T : CalendarEventAttribute
    {
        return (T)attributes[typeof(T)];
    }

    public bool TryGetAttribute<T>(out T? value) where T : CalendarEventAttribute
    {
        var success = attributes.TryGetValue(typeof(T), out var attribute);
        value = (T?)attribute;
        return success;
    }

    public bool ContainsAttribute(Type attributeType)
    {
        return attributes.ContainsKey(attributeType);
    }
}