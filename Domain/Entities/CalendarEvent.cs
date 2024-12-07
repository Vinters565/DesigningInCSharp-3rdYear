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
        var key = typeof(T);

        if (!attributes.TryAdd(key, newAttribute))
        {
            throw new InvalidOperationException($"Attribute of type {key.Name} is already added.");
        }

        return this;
    }

    public CalendarEvent RemoveAttribute<T>() where T : CalendarEventAttribute
    {
        var key = typeof(T);

        if (!attributes.ContainsKey(key))
        {
            throw new KeyNotFoundException($"Attribute of type {key.Name} does not exist.");
        }

        attributes.Remove(key);
        return this;
    }

    public T? GetAttribute<T>() where T : CalendarEventAttribute
    {
        var key = typeof(T);

        return attributes.TryGetValue(key, out var attribute) 
            ? (T?)attribute 
            : null;
    }

    public T GetRequiredAttribute<T>() where T : CalendarEventAttribute
    {
        var key = typeof(T);

        return (T)attributes[key];
    }

    public bool TryGetAttribute<T>(out T? attribute) where T : CalendarEventAttribute
    {
        var key = typeof(T);
        var success = attributes.TryGetValue(key, out var value);
        attribute = success ? (T?)value : default;
        return success;
    }

    public bool ContainsAttribute(Type attributeType) => attributes.ContainsKey(attributeType);

    public bool ContainsAttribute<T>() where T : CalendarEventAttribute 
        => attributes.ContainsKey(typeof(T));
}