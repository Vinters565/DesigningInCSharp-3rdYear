using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.ValueTypes;

public class AttributeData
{
    private readonly Dictionary<Type, IEventAttribute> attributes;

    public IReadOnlyDictionary<Type, IEventAttribute> Attributes => attributes;

    public AttributeData(Dictionary<Type, IEventAttribute>? attributes = null)
    {
        this.attributes = attributes ?? new Dictionary<Type, IEventAttribute>();
    }

    public void AddAttribute<T>(T newAttribute) where T : IEventAttribute
    {
        var key = typeof(T);

        if (!attributes.TryAdd(key, newAttribute))
        {
            throw new InvalidOperationException($"Attribute of type {key.Name} is already added.");
        }
    }

    public void UpdateAttribute<T>(T attribute) where T : IEventAttribute
    {
        var key = typeof(T);
        attributes[key] = attribute;
    }

    public void RemoveAttribute<T>() where T : IEventAttribute
    {
        var key = typeof(T);

        if (!Attributes.ContainsKey(key))
        {
            throw new KeyNotFoundException($"Attribute of type {key.Name} does not exist.");
        }

        attributes.Remove(key);
    }

    public T? GetAttribute<T>() where T : IEventAttribute
    {
        var key = typeof(T);

        return attributes.TryGetValue(key, out var attribute) 
            ? (T?)attribute 
            : default;
    }

    public T GetRequiredAttribute<T>() where T : IEventAttribute
    {
        var key = typeof(T);

        return (T)attributes[key];
    }

    public bool TryGetAttribute<T>(out T? attribute) where T : IEventAttribute
    {
        var key = typeof(T);
        var success = attributes.TryGetValue(key, out var value);
        attribute = success ? (T?)value : default;
        return success;
    }

    public bool HasAttribute(Type attributeType) => attributes.ContainsKey(attributeType);

    public bool HasAttribute<T>() where T : IEventAttribute => attributes.ContainsKey(typeof(T));

    public bool HasAttribute<T>(Func<T, bool> predicate) where T : IEventAttribute
    {
        return TryGetAttribute<T>(out var attribute)
               && predicate(attribute!);
    }

    public bool HasActiveAttribute<T>() where T : IEventAttribute
    {
        return HasAttribute<T>(attr => attr.IsActive);
    }

    public bool HasActiveAttribute<T>(Func<T, bool> predicate) where T : IEventAttribute
    {
        return HasAttribute<T>(attr => attr.IsActive && predicate(attr));
    }

    public static bool IsAttributeCreated<TAttribute>(AttributeData existedAttributes, AttributeData newAttributes)
        where TAttribute : IEventAttribute
    {
        return !existedAttributes.HasAttribute<TAttribute>() && newAttributes.HasAttribute<TAttribute>();
    }
    
    public static bool IsAttributeUpdated<TAttribute>(AttributeData existedAttributes, AttributeData newAttributes)
        where TAttribute : IEventAttribute
    {
        return existedAttributes.TryGetAttribute<TAttribute>(out var existedAttr)
               && newAttributes.TryGetAttribute<TAttribute>(out var newAttr)
               && !existedAttr!.Equals(newAttr!);
    }
    
    public static bool IsAttributeDeleted<TAttribute>(AttributeData existedAttributes, AttributeData newAttributes)
        where TAttribute : IEventAttribute
    {
        return existedAttributes.HasAttribute<TAttribute>() && !newAttributes.HasAttribute<TAttribute>();
    }
}