using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.Entities;

public class AttributeData
{
    public Dictionary<Type, IEventAttribute> Attributes { get; set; }

    public AttributeData(Dictionary<Type, IEventAttribute>? attributes = null)
    {
        this.Attributes = attributes ?? new Dictionary<Type, IEventAttribute>();
    }

    public AttributeData()
    {
        Attributes = new Dictionary<Type, IEventAttribute>();
    }

    public void AddAttribute<T>(T newAttribute) where T : IEventAttribute
    {
        var key = typeof(T);

        if (!Attributes.TryAdd(key, newAttribute))
        {
            throw new InvalidOperationException($"Attribute of type {key.Name} is already added.");
        }
    }

    public void UpdateAttribute<T>(T attribute) where T : IEventAttribute
    {
        var key = typeof(T);
        Attributes[key] = attribute;
    }

    public void RemoveAttribute<T>() where T : IEventAttribute
    {
        var key = typeof(T);

        if (!Attributes.ContainsKey(key))
        {
            throw new KeyNotFoundException($"Attribute of type {key.Name} does not exist.");
        }

        Attributes.Remove(key);
    }

    public T? GetAttribute<T>() where T : IEventAttribute
    {
        var key = typeof(T);

        return Attributes.TryGetValue(key, out var attribute) 
            ? (T?)attribute 
            : default;
    }

    public T GetRequiredAttribute<T>() where T : IEventAttribute
    {
        var key = typeof(T);

        return (T)Attributes[key];
    }

    public bool TryGetAttribute<T>(out T? attribute) where T : IEventAttribute
    {
        var key = typeof(T);
        var success = Attributes.TryGetValue(key, out var value);
        attribute = success ? (T?)value : default;
        return success;
    }

    public bool HasAttribute(Type attributeType) => Attributes.ContainsKey(attributeType);

    public bool HasAttribute<T>() where T : IEventAttribute => Attributes.ContainsKey(typeof(T));

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