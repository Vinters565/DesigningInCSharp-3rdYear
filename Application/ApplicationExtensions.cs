using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SchedulePlanner.Application.Users;

namespace SchedulePlanner.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddJwtAuth(
        this IServiceCollection services,
        IConfigurationSection jwtSection
    )
    {
        var jwtOptions = new JwtOptions();
        jwtSection.Bind(jwtOptions);
        jwtOptions.SecretKey = "zosdfglkzjdsfgl3541f353dgzdkjmdfgnzldjnzd65415412"; // Todo move to env

        services.Configure<JwtOptions>(options =>
        {
            options.Expires = jwtOptions.Expires;
            options.SecretKey = jwtOptions.SecretKey;
        });

        services.AddScoped<JwtService>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey)
                    ),
                }
            );

        return services;
    }
}
