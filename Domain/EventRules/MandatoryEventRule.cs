using System.Reflection;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Domain.EventRules;

public class MandatoryEventRule : IEventRule
{
    public int Priority => 0;

    private readonly Type[] mandatoryAttributeTypes;
    
    public MandatoryEventRule()
    {
        mandatoryAttributeTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IMandatoryEventAttribute).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToArray();
    }
    
    public bool Check(CalendarEvent newCalendarEvent)
    {
        return mandatoryAttributeTypes.All(newCalendarEvent.HasAttribute);
    }
}