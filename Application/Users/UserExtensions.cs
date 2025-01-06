using SchedulePlanner.Application.Users.Responses;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.Users;

public static class UserExtensions
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Username = user.Username
        };
    }
}