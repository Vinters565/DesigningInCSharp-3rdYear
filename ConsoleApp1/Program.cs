using SchedulePlanner.Domain;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.EventRules;
using SchedulePlanner.Domain.Interfaces;

var ruleChain = new EventRuleChain()
    .AddNextEventRule(new MandatoryEventRule())
    .AddNextEventRule(new TimeEventRule())
    .AddNextEventRule(new SingleOnlyEventRule(new CalendarEventRepository()));

var newEvent = new CalendarEventBuilder(new CalendarEvent(Guid.NewGuid()), ruleChain)
    .AddAttribute(new StartDateEventAttribute(new DateTime(2024, 12, 6, 15, 0, 0)))
    .AddAttribute(new EndDateEventAttribute(new DateTime(2024, 12, 6, 16, 30, 0)))
    //.AddAttribute(new SingleOnlyEventAttribute(true))
    //.RemoveAttribute<EndDateEventAttribute>()
    .Apply();
