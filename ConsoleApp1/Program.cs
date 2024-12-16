using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.EventRules;
using SchedulePlanner.Domain;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.EventRules;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Infrastructure;

var services = new ServiceCollection();

services.AddInfrastructureLayer();
services.AddApplicationLayer();

var serviceProvider = services.BuildServiceProvider();

var ruleChecker = serviceProvider.GetRequiredService<IEventRuleChecker>();
var repository = serviceProvider.GetRequiredService<ICalendarEventRepository>();
repository.AddEvent(
    new CalendarEvent(
        Guid.NewGuid(),
        new DateTime(2024, 12, 6, 10, 0, 0),
        new DateTime(2024, 12, 6, 12, 0, 0)));

repository.AddEvent(
    new CalendarEvent(
        Guid.NewGuid(),
        new DateTime(2024, 12, 6, 14, 0, 0),
        new DateTime(2024, 12, 6, 18, 0, 0))
        .AddAttribute(new SingleOnlyEventAttribute(true)));

repository.AddEvent(
    new CalendarEvent(
        Guid.NewGuid(),
        new DateTime(2024, 12, 6, 19, 0, 0),
        new DateTime(2024, 12, 6, 21, 0, 0)));

var newEventResult = new CalendarEventBuilder(new CalendarEvent(
        Guid.NewGuid(),
        new DateTime(2024, 12, 6, 15, 0, 0),
        new DateTime(2024, 12, 6, 16, 30, 0)), ruleChecker)
    .AddAttribute(new SingleOnlyEventAttribute(true))
    .ApplyRules();

if (!newEventResult.IsError)
{
    repository.AddEvent(newEventResult.Value);
}

foreach (var ev in repository.GetAllEvents())
{
    Console.WriteLine($"{ev.Id} {ev.StartDate.ToString("yyyy-MM-ddTHH:mm:ss")} {ev.EndDate.ToString("yyyy-MM-ddTHH:mm:ss")}");
}

if (newEventResult.IsError) Console.WriteLine(newEventResult.Error.Message);
else Console.WriteLine("Атрибуты успешно применены");
