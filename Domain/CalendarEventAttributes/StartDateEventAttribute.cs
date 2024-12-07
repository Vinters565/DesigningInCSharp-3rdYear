using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.CalendarEventAttributes;

public class StartDateEventAttribute(DateTime start) : CalendarEventAttribute("StartDate"), IMandatory
{
    public DateTime StartDate { get; } = start;
}