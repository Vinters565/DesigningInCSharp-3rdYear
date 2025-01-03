using SchedulePlanner.Application;
using SchedulePlanner.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using System;
using Api.Extensions;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.CalendarEvents.EventRules;
using SchedulePlanner.Application.JsonConverters;
using SchedulePlanner.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new EventAttributeIReadOnlyDictionaryConverter());
        options.JsonSerializerOptions.Converters.Add(new EventAttributeDictionaryConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithJwtSecurity();

builder.Services.AddInfrastructureLayer();
builder.Services.AddJwtAuth(builder.Configuration.GetSection("JwtOptions"));
builder.Services.AddApplicationLayer();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
