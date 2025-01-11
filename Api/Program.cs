using Api.Extensions;
using Microsoft.OpenApi.Models;
using SchedulePlanner.Application;
using SchedulePlanner.Infrastructure;
using SchedulePlanner.Utils.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureGlobalJsonConverters();
  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Calendar App API", Version = "v1" });
    options.AddJwtSecurity();
});

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
