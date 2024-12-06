namespace SchedulePlanner.Domain.Common;

public abstract class CalendarEventAttribute(string name, object value)
{
    public string Name { get; } = name;
    public object Value { get; } = value;
}