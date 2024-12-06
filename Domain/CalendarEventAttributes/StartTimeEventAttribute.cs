using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.CalendarEventAttributes;

public class StartTimeEventAttribute : CalendarEventAttribute, IMandatory
{
    public StartTimeEventAttribute(TimeOnly start) : base("StartTime", start)
    {

    }
}