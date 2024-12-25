using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.Entities;

public class CalendarEvent : Entity<Guid>
{
    public Guid UserId { get; }
    
    public DateTime StartDate { get; private set; }
    
    public DateTime EndDate { get; private set; }
    
    private Dictionary<Type, IEventAttribute> attributes;
    public IReadOnlyDictionary<Type, IEventAttribute> Attributes => attributes;

    public CalendarEvent(Guid userId, DateTime startDate, DateTime endDate)
        : this(userId, Guid.NewGuid(), startDate, endDate)
    {
    }

    public CalendarEvent(
        Guid userId, 
        DateTime startDate, 
        DateTime endDate, 
        Dictionary<Type, IEventAttribute> attributes) 
        : this(userId, Guid.NewGuid(), startDate, endDate, attributes)
    {
    }

    public CalendarEvent(
        Guid userId, 
        Guid entityId, 
        DateTime startDate, 
        DateTime endDate, 
        Dictionary<Type, IEventAttribute>? attributes = null) 
        : base(entityId)
    {
        UserId = userId;
        
        StartDate = startDate;
        EndDate = endDate;
        ValidateDates();
        
        this.attributes = attributes ?? new Dictionary<Type, IEventAttribute>();
    }

    public CalendarEvent AddAttribute<T>(T newAttribute) where T : IEventAttribute
    {
        var key = typeof(T);

        if (!attributes.TryAdd(key, newAttribute))
        {
            throw new InvalidOperationException($"Attribute of type {key.Name} is already added.");
        }

        return this;
    }

    public void UpdateAttribute<T>(T attribute) where T : IEventAttribute
    {
        var key = typeof(T);
        attributes[key] = attribute;
    }

    public CalendarEvent RemoveAttribute<T>() where T : IEventAttribute
    {
        var key = typeof(T);

        if (!attributes.ContainsKey(key))
        {
            throw new KeyNotFoundException($"Attribute of type {key.Name} does not exist.");
        }

        attributes.Remove(key);
        return this;
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

    public void Update(DateTime? start, DateTime? end, Dictionary<Type, IEventAttribute>? newAttributes)
    {
        if (start != null) StartDate = start.Value;
        if (end != null) EndDate = end.Value;
        ValidateDates();

        if (newAttributes != null) attributes = newAttributes;
    }

    private void ValidateDates()
    {
        if (StartDate > EndDate) throw new ArgumentException("Start date must be earlier than the end");
    }
}