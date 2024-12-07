using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.Entities.CalendarEventAttributes;

public class EndDateEventAttribute(DateTime endDate) : CalendarEventAttribute("EndDate"), IMandatoryAttribute
{
    public DateTime EndDate { get; } = endDate;
}