using SchedulePlanner.Domain;
using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Application.CalendarEvents.EventAttributes.Dtos;

public class EventAttributeDto
{
    public string Name { get; init; }
    
    public IReadOnlyCollection<FieldMetadata> Fields { get; init; }
}