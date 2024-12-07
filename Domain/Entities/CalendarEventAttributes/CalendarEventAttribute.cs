namespace SchedulePlanner.Domain.Entities.CalendarEventAttributes;

public abstract class CalendarEventAttribute(string name)
{
    public string Name { get; } = name;
}