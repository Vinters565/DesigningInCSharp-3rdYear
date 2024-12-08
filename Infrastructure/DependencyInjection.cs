using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Domain.Interfaces;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddTransient<ICalendarEventRepository, CalendarEventRepository>();

        return services;
    }
}