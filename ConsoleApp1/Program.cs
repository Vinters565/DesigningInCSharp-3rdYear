using SchedulePlanner.Domain;
using SchedulePlanner.Domain.CalendarEventAttributes;
using SchedulePlanner.Domain.Entities;

var newEvent = new CalendarEventBuilder( new CalendarEvent())
    .AddAttribute(new StartDateEventAttribute(new DateTime(2024, 12, 6, 15, 0, 0)))
    .AddAttribute(new EndDateEventAttribute(new DateTime(2024, 12, 6, 16, 30, 0)))
    //.AddAttribute(new SingleOnlyEventAttribute(true))
    //.RemoveAttribute<EndDateEventAttribute>()
    .Apply();
