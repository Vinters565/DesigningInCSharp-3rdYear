using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents;

public static class CalendarEventExtensions
{
    public static CalendarEventDto ToDto(this CalendarEvent calendarEvent)
    {
        return new CalendarEventDto
        {
            Id = calendarEvent.Id,
            UserId = calendarEvent.UserId,
            Start = calendarEvent.StartDate,
            End = calendarEvent.EndDate,
            Attributes = calendarEvent.AttributeData.Attributes
        };
    }
}