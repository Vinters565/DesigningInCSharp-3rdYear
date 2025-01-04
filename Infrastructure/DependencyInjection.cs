using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.Subscriptions;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Infrastructure.Repositories;

namespace SchedulePlanner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddRepository();

        return services;
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=calendar_app.db"));
        services.AddScoped<ICalendarEventRepository, CalendarEventRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        
        return services;
    }
}

//"Data Source=calendar_app.db"
