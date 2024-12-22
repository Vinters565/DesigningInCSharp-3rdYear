using SchedulePlanner.Application.Users.Requests;
using SchedulePlanner.Application.Users.Responses;
using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.Users;

public class UserService(IUserRepository userRepository, IPasswordHasher passwordHasher) : IUserService
{
    public async Task<Result<LoginUserResponse>> LoginAsync(LoginUserRequest request)
    {
        // TODO
        throw new NotImplementedException();

        var user = await userRepository.GetByUsernameAsync(request.Username);
        if (user == null)
            return Error.NotFound($"User with name {request.Username} not found");
    }

    public async Task<Result> RegisterAsync(RegisterUserRequest request)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username);
        if (user != null)
            return Error.Failure($"User with name {request.Username} already exists");

        var userID = Guid.NewGuid();
        user = await userRepository.GetByIDAsync(userID);
        if (user != null)
            return Error.Failure($"User with ID {userID} already exists");

        var passwordHash = passwordHasher.Hash(request.Password);
        user = new(userID, request.Username, request.DisplayedName, passwordHash);
        await userRepository.CreateAsync(user);

        return Result.Success();
    }
}
