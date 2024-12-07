using SchedulePlanner.Domain.Common;

namespace SchedulePlanner.Domain.Entities.CalendarEventAttributes;

public class SingleOnlyEventAttribute(bool isSingleOnly) : CalendarEventAttribute("SingleOnly")
{
    public bool IsSingleOnly { get; } = isSingleOnly;
}