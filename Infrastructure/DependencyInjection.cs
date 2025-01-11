using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application.CalendarEvents.EventAttributes;
using SchedulePlanner.Utils.Extensions;

namespace SchedulePlanner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.AddRepositories(assembly);
        services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=calendar_app.db"));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        services.AddByConvention(assembly, "Repository", ServiceLifetime.Scoped);
        return services;
    }
}
