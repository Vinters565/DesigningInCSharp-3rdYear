using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.CalendarEventAttributes;

public class DateEventAttribute(DateOnly date) : CalendarEventAttribute("Date", date), IMandatory
{
}