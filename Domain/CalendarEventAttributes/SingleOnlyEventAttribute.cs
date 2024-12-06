using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.CalendarEventAttributes;

public class SingleOnlyEventAttribute(bool isSingleOnly) : CalendarEventAttribute("SingleOnly", isSingleOnly)
{
}