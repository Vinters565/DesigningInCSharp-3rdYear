using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Rules;

public class TimeRule : Rule
{
    public override int VerificationPriority => 1;

    public override bool Check(CalendarEvent newCalendarEvent)
    {
        var startTime = (TimeOnly)newCalendarEvent.Attributes["StartTime"].Value;
        var endTime = (TimeOnly)newCalendarEvent.Attributes["EndTime"].Value;

        return startTime < endTime;
    }
}