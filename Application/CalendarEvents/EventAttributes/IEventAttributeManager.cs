using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Utils.Result;

namespace SchedulePlanner.Application.CalendarEvents.EventAttributes;

public interface IEventAttributeManager
{
    Task<Result> UpdateAsync(CalendarEvent calendarEvent, Dictionary<Type, IEventAttribute> newAttributes);
}