using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents.Dtos;

public class UpdateCalendarEventRequest
{
    public string? Name { get; init; }
    
    public DateTime? Start { get; init; }
    
    public DateTime? End { get; init; }
    
    public IReadOnlyDictionary<Type, IEventAttribute>? Attributes { get; init; }
}