using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.EventRules;
using SchedulePlanner.Domain;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.EventRules;
using SchedulePlanner.Infrastructure;

var services = new ServiceCollection();

services.AddInfrastructureLayer();
services.AddApplicationLayer();

var serviceProvider = services.BuildServiceProvider();

var ruleChecker = serviceProvider.GetRequiredService<IEventRuleChecker>();

var newEventResult = new CalendarEventBuilder(new CalendarEvent(
        Guid.NewGuid(), 
        new DateTime(2024, 12, 6, 15, 0, 0),
        new DateTime(2024, 12, 6, 16, 30, 0)), ruleChecker)
    .AddAttribute(new SingleOnlyEventAttribute(true))
    .ApplyRules();

if (newEventResult.IsError) Console.WriteLine(newEventResult.Error.Message);
else Console.WriteLine("Атрибуты успешно применены");
