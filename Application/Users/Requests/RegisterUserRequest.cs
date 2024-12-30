namespace SchedulePlanner.Application.Users.Requests;

public record class RegisterUserRequest(string Username, string DisplayedName, string Password);
