using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.CalendarEventAttributes;

public class StartTimeEventAttribute(TimeOnly start) : CalendarEventAttribute("StartTime"), IMandatory
{
    public TimeOnly StartTime { get; } = start;
}