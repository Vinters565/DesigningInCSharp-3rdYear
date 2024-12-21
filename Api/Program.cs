using SchedulePlanner.Application.Converters;
using SchedulePlanner.Application;
using SchedulePlanner.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Application.EventRules;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new EventAttributeIReadOnlyDictionaryConverter());
        options.JsonSerializerOptions.Converters.Add(new EventAttributeDictionaryConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureLayer();
builder.Services.AddApplicationLayer();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var ruleChecker = app.Services.GetRequiredService<IEventRuleChecker>();
var repository = app.Services.GetRequiredService<ICalendarEventRepository>();
var events = new List<CalendarEvent>();
events.Add(
    new CalendarEvent(
        Guid.NewGuid(),
        new DateTime(2024, 12, 6, 12, 0, 0),
        new DateTime(2024, 12, 6, 15, 0, 0)));

events.Add(
    new CalendarEvent(
        Guid.NewGuid(),
        new DateTime(2024, 12, 6, 15, 0, 0),
        new DateTime(2024, 12, 6, 18, 0, 0))
        .AddAttribute(new SingleOnlyEventAttribute(true))
        .AddAttribute(new PublicityAttribute(true)));

events.Add(
    new CalendarEvent(
        Guid.NewGuid(),
        new DateTime(2024, 12, 6, 19, 0, 0),
        new DateTime(2024, 12, 6, 21, 0, 0)));

foreach (var ev in events)
{
    repository.AddEvent(ev);
}


foreach (var ev in repository.GetAllEvents())
{
    Console.WriteLine($"{ev.Id} {ev.StartDate.ToString("yyyy-MM-ddTHH:mm:ss")} {ev.EndDate.ToString("yyyy-MM-ddTHH:mm:ss")} ");
    var atributs = ev.Attributes.Select(x => x.Key.Name).ToList();
    if (atributs.Count != 0)
    {
        foreach (var atribute in atributs)
        {
            Console.Write(atribute + ", ");
        }
        Console.Write("\n");
    }
    repository.DeleteEvent(ev.Id.ToString("D"));
}

app.Run();