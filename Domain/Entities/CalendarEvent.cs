using SchedulePlanner.Domain.Common;

namespace SchedulePlanner.Domain.Entities;

public class CalendarEvent : Entity<Guid>
{
    private readonly Dictionary<string, CalendarEventAttribute> attributes = new();
    
    public IReadOnlyDictionary<string, CalendarEventAttribute> Attributes => attributes;
    
    public CalendarEvent() : base(Guid.NewGuid())
    {
        
    }

    public CalendarEvent AddAttribute(CalendarEventAttribute newAttribute)
    {
        attributes.Add(newAttribute.Name, newAttribute);

        return this;
    }
}