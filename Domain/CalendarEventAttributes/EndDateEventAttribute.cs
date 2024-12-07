using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.CalendarEventAttributes;

public class EndDateEventAttribute(DateTime endDate) : CalendarEventAttribute("EndDate"), IMandatory
{
    public DateTime EndDate { get; } = endDate;
}