using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents.EventAttributes;

public interface IEventAttributeManager
{
    Task<Result> UpdateAsync(CalendarEvent calendarEvent, Dictionary<Type, IEventAttribute> newAttributes);
}