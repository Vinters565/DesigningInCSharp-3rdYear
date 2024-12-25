using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents.Dtos;

public class CalendarEventDto
{
    public Guid Id { get; init; }
    
    public Guid UserId { get; init; }
    
    public DateTime Start { get; init; }
    
    public DateTime End { get; init; }
    
    public IReadOnlyDictionary<Type, IEventAttribute> Attributes { get; init; } = null!;
}