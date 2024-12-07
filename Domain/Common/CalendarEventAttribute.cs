namespace SchedulePlanner.Domain.Common;

public abstract class CalendarEventAttribute(string name)
{
    public string Name { get; } = name;
}