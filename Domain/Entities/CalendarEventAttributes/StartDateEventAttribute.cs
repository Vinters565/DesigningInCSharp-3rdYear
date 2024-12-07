using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.Entities.CalendarEventAttributes;

public class StartDateEventAttribute(DateTime start) : CalendarEventAttribute("StartDate"), IMandatoryAttribute
{
    public DateTime StartDate { get; } = start;
}