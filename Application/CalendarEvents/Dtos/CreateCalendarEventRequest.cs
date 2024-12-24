using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Application.CalendarEvents.Dtos;

public class CreateCalendarEventRequest
{
    public DateTime Start { get; init; }
    
    public DateTime End { get; init; }
    
    public IReadOnlyDictionary<Type, IEventAttribute> Attributes { get; init; }
}