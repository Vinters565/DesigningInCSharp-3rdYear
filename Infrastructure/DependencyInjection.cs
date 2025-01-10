using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.CalendarEvents.EventAttributes;
using SchedulePlanner.Application.Users;
using SchedulePlanner.Application.Subscriptions;
using SchedulePlanner.Infrastructure.Repositories;
using SchedulePlanner.Infrastructure.Services;

namespace SchedulePlanner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=calendar_app.db"));
        services.AddRepositories();
        services.AddSingleton<IEventAttributesRegistry, EventAttributesRegistry>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICalendarEventRepository, CalendarEventRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        
        return services;
    }
}
