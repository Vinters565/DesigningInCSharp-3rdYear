using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Application.JsonConverters;
using SchedulePlanner.Utils.Extensions;

namespace Api.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureGlobalJsonConverters(this IServiceCollection services)
    {
        var converters = Assembly.Load("SchedulePlanner.Application")
            .GetAllImplementationsOf<JsonConverter>()
            .Select(converterType => 
                (JsonConverter?)Activator.CreateInstance(converterType) 
                ?? throw new Exception("Cannot create JsonConverter from assembly"))
            .ToList();
        
        services.Configure<JsonSerializerOptions>(options =>
        {
            converters.ForEach(c => options.Converters.Add(c));
        });
        
        services.Configure<JsonOptions>(options =>
        {
            converters.ForEach(c => options.JsonSerializerOptions.Converters.Add(c));
        });

        return services;
    }
}