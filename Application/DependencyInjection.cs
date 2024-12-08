using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application.EventRules;
using SchedulePlanner.Domain.EventRules;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddEventRuleChain();
        
        return services;
    }

    private static IServiceCollection AddEventRuleChain(this IServiceCollection services)
    {
        services.AddSingleton<IEventRuleChain>(provider =>
        {
            var calendarEventRepository = provider.GetRequiredService<ICalendarEventRepository>();
    
            return new EventRuleChain()
                .AddNextEventRule(new MandatoryEventRule())
                .AddNextEventRule(new TimeEventRule())
                .AddNextEventRule(new SingleOnlyEventRule(calendarEventRepository));
        });

        return services;
    }
}