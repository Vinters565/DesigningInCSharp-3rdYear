using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.Users;

public class JwtService(IOptions<JwtOptions> options)
{
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new("id", user.Id.ToString()),
            new("userName", user.Username),
        };

        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(options.Value.Expires),
            claims: claims,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey!)),
                SecurityAlgorithms.HmacSha256
            )
        );
        
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
