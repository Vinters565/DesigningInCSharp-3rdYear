using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.CalendarEventAttributes;

public class EndTimeEventAttribute(TimeOnly endTime) : CalendarEventAttribute("EndTime", endTime), IMandatory
{
}