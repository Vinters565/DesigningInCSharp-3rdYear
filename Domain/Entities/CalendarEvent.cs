using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.Entities;

public class CalendarEvent : Entity<Guid>
{
    public Guid UserId { get; }
    
    public DateTime StartDate { get; private set; }
    
    public DateTime EndDate { get; private set; }
    
    public AttributeData AttributeData { get; private set; }

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

        AttributeData = new AttributeData(attributes);
    }

    public void Update(DateTime? start, DateTime? end)
    {
        if (start != null) StartDate = start.Value;
        if (end != null) EndDate = end.Value;
        ValidateDates();
    }

    public void UpdateAttributes(Dictionary<Type, IEventAttribute>? newAttributes)
    {
        if (newAttributes != null) AttributeData = new AttributeData(newAttributes);
    }

    private void ValidateDates()
    {
        if (StartDate > EndDate) throw new ArgumentException("Start date must be earlier than the end");
    }
}