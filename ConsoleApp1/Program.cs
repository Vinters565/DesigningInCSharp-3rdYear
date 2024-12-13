using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application;
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

var newEventResult = new CalendarEventBuilder(new CalendarEvent(Guid.NewGuid()), ruleChecker)
    .AddAttribute(new StartDateEventAttribute(new DateTime(2024, 12, 6, 15, 0, 0)))
    .AddAttribute(new EndDateEventAttribute(new DateTime(2024, 12, 6, 16, 30, 0)))
    .AddAttribute(new SingleOnlyEventAttribute(true))
    .RemoveAttribute<EndDateEventAttribute>()
    .ApplyRules();

if (newEventResult.IsError) Console.WriteLine(newEventResult.Error.Message);
else Console.WriteLine("Атрибуты успешно применены");
