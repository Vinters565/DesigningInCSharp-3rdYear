using SchedulePlanner.Domain;
using SchedulePlanner.Domain.CalendarEventAttributes;
using SchedulePlanner.Domain.Entities;

var newEvent = new CalendarEventBuilder( new CalendarEvent())
    .AddAttribute(new DateEventAttribute(new DateOnly(2024, 12, 6)))
    .AddAttribute(new StartTimeEventAttribute(new TimeOnly(15, 0)))
    .AddAttribute(new EndTimeEventAttribute(new TimeOnly(16, 30)))
    //.AddAttribute(new SingleOnlyEventAttribute(true))
    .Apply();
