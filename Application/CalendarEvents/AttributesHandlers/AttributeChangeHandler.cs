using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Domain.ValueTypes;

namespace SchedulePlanner.Application.CalendarEvents.AttributesHandlers;

public abstract class AttributeChangeHandler<TAttribute> : IAttributeChangeHandler 
    where TAttribute : IEventAttribute
{
    public virtual async Task HandleAsync(AttributeData before, AttributeData after, CalendarEvent calendarEvent)
    {
        var existed = before.TryGetAttribute<TAttribute>(out var existedAttribute);
        var hasNow = after.TryGetAttribute<TAttribute>(out var currentAttribute);
        
        if (!existed && hasNow)
        {
            await OnAddAsync(currentAttribute!, calendarEvent);
            return;
        }
        
        if (existed && !hasNow)
        {
            await OnDeleteAsync(existedAttribute!, calendarEvent);
            return;
        }
        
        if (IsAttributeUpdated(existed, hasNow, existedAttribute, currentAttribute))
        {
            await OnUpdateAsync(existedAttribute!, currentAttribute!, calendarEvent);
            return;
        }
    }
    
    protected virtual Task OnAddAsync(TAttribute addedAttribute, CalendarEvent calendarEvent)
    {
        return  Task.CompletedTask;
    }
    
    protected virtual Task OnUpdateAsync(TAttribute before, TAttribute after, CalendarEvent calendarEvent)
    {
        return  Task.CompletedTask;
    }
    
    protected virtual Task OnDeleteAsync(TAttribute deletedAttribute, CalendarEvent calendarEvent)
    {
        return  Task.CompletedTask;
    }
    
    private bool IsAttributeUpdated(bool existed, bool hasNow, TAttribute? before, TAttribute? after) => 
        existed && hasNow && !before!.Equals(after!);
}