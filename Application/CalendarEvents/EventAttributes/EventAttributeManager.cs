using SchedulePlanner.Application.CalendarEvents.AttributeActions;
using SchedulePlanner.Application.CalendarEvents.EventRules;
using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents.EventAttributes;

public class EventAttributeManager(
    IAttributeActionsApplier attributeActionsApplier,
    IEventRuleChecker ruleChecker) : IEventAttributeManager
{
    public async Task<Result> UpdateAsync(CalendarEvent calendarEvent, Dictionary<Type, IEventAttribute> newAttributes)
    {
        var oldAttributes = calendarEvent.AttributeData;
        calendarEvent.UpdateAttributes(newAttributes);
        var failedRule = await ruleChecker.CheckAsync(calendarEvent);

        if (failedRule != null)
        {
            return Error.Failure($"Правило '{failedRule}' нарушено");
        }

        await attributeActionsApplier.Apply(oldAttributes, calendarEvent.AttributeData, calendarEvent);
        return Result.Success();
    }
}