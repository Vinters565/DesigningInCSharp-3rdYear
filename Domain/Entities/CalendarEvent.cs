using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.EventAttributes.Attributes;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Domain.ValueTypes;

namespace SchedulePlanner.Domain.Entities;

public class CalendarEvent : Entity<Guid>
{
    public string Name { get; private set; }

    public Guid UserId { get; }
    
    public DateTime StartDate { get; private set; }
    
    public DateTime EndDate { get; private set; }
    
    public AttributeData AttributeData { get; private set; }

    public CalendarEvent(string name, Guid userId, DateTime startDate, DateTime endDate)
        : this(name, userId, Guid.NewGuid(), startDate, endDate)
    {
    }

    public CalendarEvent(
        string name,
        Guid userId, 
        Guid entityId, 
        DateTime startDate, 
        DateTime endDate, 
        Dictionary<Type, IEventAttribute>? attributes = null) 
        : base(entityId)
    {
        Name = name;
        UserId = userId;
        
        StartDate = startDate;
        EndDate = endDate;
        ValidateDates();

        AttributeData = new AttributeData(attributes);
    }
    
    private CalendarEvent() : base(Guid.NewGuid()) { } // EF Core

    public void Update(string? name, DateTime? start, DateTime? end)
    {
        if (name != null) Name = name;
        if (start != null) StartDate = start.Value;
        if (end != null) EndDate = end.Value;
        ValidateDates();
    }

    public void UpdateAttributes(Dictionary<Type, IEventAttribute>? newAttributes)
    {
        if (newAttributes != null) AttributeData = new AttributeData(newAttributes);
    }

    public bool IsPublic()
    {
        return AttributeData.HasActiveAttribute<PublicityEventAttribute>();
    }

    private void ValidateDates()
    {
        if (StartDate > EndDate) throw new ArgumentException("Start date must be earlier than the end");
    }
}