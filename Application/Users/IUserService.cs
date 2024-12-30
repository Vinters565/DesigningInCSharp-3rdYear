using SchedulePlanner.Application.Users.Requests;
using SchedulePlanner.Application.Users.Responses;
using SchedulePlanner.Domain.Common.Results;

namespace SchedulePlanner.Application.Users;

public interface IUserService
{
    Task<Result> RegisterAsync(RegisterUserRequest request);
    Task<Result<LoginUserResponse>> LoginAsync(LoginUserRequest request);
}
