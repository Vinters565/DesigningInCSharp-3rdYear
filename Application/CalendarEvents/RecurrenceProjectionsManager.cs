using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Enums;
using SchedulePlanner.Domain.EventAttributes.Attributes;

namespace SchedulePlanner.Application.CalendarEvents;

public class RecurrenceProjectionsManager : IRecurrenceProjectionsManager
{
    public List<CalendarEvent> GetProjections(IReadOnlyCollection<CalendarEvent> recurringEvents, DateTime maxUntil)
    {
        var projections = new List<CalendarEvent>();

        foreach (var recurringEvent in recurringEvents)
        {
            if (!recurringEvent.AttributeData.TryGetAttribute<RecurrenceEventAttribute>(out var recurrenceAttribute))
                throw new ArgumentException("Calendar event does not contains recurrence attribute");

            var recurrenceType = recurrenceAttribute!.Type;
            
            var until = recurrenceAttribute.Until;
            if (until == null || until > maxUntil)
            {
                until = maxUntil;
            }
            
            projections.AddRange(GetProjections(recurringEvent, recurrenceType, until.Value));
        }

        return projections;
    }

    private static IEnumerable<CalendarEvent> GetProjections(
        CalendarEvent recurringEvent, RecurrenceType recurrenceType, DateTime until)
    {
        var recurrenceTime = GetRecurrenceTime(recurrenceType);

        var nextProjectionStart = recurringEvent.StartDate + recurrenceTime;
        var nextProjectionEnd = recurringEvent.EndDate + recurrenceTime;

        while (nextProjectionStart < until)
        {
            var projection = new CalendarEvent(recurringEvent.Name, recurringEvent.UserId, recurringEvent.Id,
                nextProjectionStart, nextProjectionEnd, recurringEvent.AttributeData.Attributes.ToDictionary());

            yield return projection;
            
            nextProjectionStart += recurrenceTime;
            nextProjectionEnd += recurrenceTime;
        }
    }

    private static TimeSpan GetRecurrenceTime(RecurrenceType recurrenceType)
    {
        return recurrenceType switch
        {
            RecurrenceType.EveryDay => TimeSpan.FromDays(1),
            RecurrenceType.EveryWeek => TimeSpan.FromDays(7),
            RecurrenceType.EveryMonth => TimeSpan.FromDays(30),
            _ => throw new ArgumentOutOfRangeException(nameof(recurrenceType), recurrenceType, null)
        };
    }
}