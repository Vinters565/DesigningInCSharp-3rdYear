using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Application.Dto;

public class CalendarEventDto
{
    public Guid UserId { get; init; }
    
    public DateTime Start { get; init; }
    
    public DateTime End { get; init; }
    
    public IReadOnlyDictionary<Type, IEventAttribute> Attributes { get; init; } = null!;
}