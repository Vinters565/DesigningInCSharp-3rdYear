using SchedulePlanner.Application.CalendarEvents.AttributesHandlers;
using SchedulePlanner.Application.CalendarEvents.EventRules;
using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents.EventAttributes;

public class EventAttributeManager(
    IAttributesChangesHandler attributesChangesHandler,
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

        await attributesChangesHandler.HandleAsync(oldAttributes, calendarEvent.AttributeData, calendarEvent);
        return Result.Success();
    }
}