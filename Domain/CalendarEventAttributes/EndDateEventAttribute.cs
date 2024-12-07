using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.CalendarEventAttributes;

public class EndDateEventAttribute(DateTime endDate) : CalendarEventAttribute("EndDate"), IMandatoryAttribute
{
    public DateTime EndDate { get; } = endDate;
}