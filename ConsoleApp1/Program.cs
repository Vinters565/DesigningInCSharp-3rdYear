using SchedulePlanner.Domain;
using SchedulePlanner.Domain.Builders;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Entities.CalendarEventAttributes;

var newEvent = new CalendarEventBuilder( new CalendarEvent(Guid.NewGuid()))
    .AddAttribute(new StartDateEventAttribute(new DateTime(2024, 12, 6, 15, 0, 0)))
    .AddAttribute(new EndDateEventAttribute(new DateTime(2024, 12, 6, 16, 30, 0)))
    //.AddAttribute(new SingleOnlyEventAttribute(true))
    //.RemoveAttribute<EndDateEventAttribute>()
    .Apply();
