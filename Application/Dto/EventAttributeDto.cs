using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Application.Dto;

public class EventAttributeDto
{
    public IEventAttribute Value { get; init; }
}