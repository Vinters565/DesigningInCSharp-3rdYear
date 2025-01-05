using System.Drawing;
using SchedulePlanner.Application.Users.Requests;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Domain.ValueTypes;
using SchedulePlanner.Utils.Result;

namespace SchedulePlanner.Application.Users;

public class UserService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    JwtService jwtService
) : IUserService
{
    public async Task<Result<string>> LoginAsync(LoginUserRequest request)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username);
        if (user == null)
            return Error.NotFound($"User with name {request.Username} not found");

        if (!passwordHasher.Verify(request.Password, user.PasswordHash))
            return Error.Failure("Wrong password");

        var token = jwtService.GenerateToken(user);
        return token;
    }

    public async Task<Result<string>> RegisterAsync(RegisterUserRequest request)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username);
        if (user != null)
            return Error.Failure($"User with name {request.Username} already exists");

        var userID = Guid.NewGuid();
        user = await userRepository.GetByIdAsync(userID);
        if (user != null)
            return Error.Failure($"User with ID {userID} already exists");

        var passwordHash = passwordHasher.Hash(request.Password);
        var userSettings = new UserSettings(request.DisplayedName);
        user = new User(userID, request.Username, passwordHash, userSettings);
        userRepository.Create(user);
        await userRepository.SaveChangesAsync();

        var token = jwtService.GenerateToken(user);
        return token;
    }
}
