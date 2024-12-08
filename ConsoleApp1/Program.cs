using Application;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Domain;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.EventRules;

var services = new ServiceCollection();

services.AddInfrastructureLayer();
services.AddApplicationLayer();

var serviceProvider = services.BuildServiceProvider();

var ruleChain = serviceProvider.GetRequiredService<IEventRuleChain>();

var newEvent = new CalendarEventBuilder(new CalendarEvent(Guid.NewGuid()), ruleChain)
    .AddAttribute(new StartDateEventAttribute(new DateTime(2024, 12, 6, 15, 0, 0)))
    .AddAttribute(new EndDateEventAttribute(new DateTime(2024, 12, 6, 16, 30, 0)))
    //.AddAttribute(new SingleOnlyEventAttribute(true))
    //.RemoveAttribute<EndDateEventAttribute>()
    .Apply();
