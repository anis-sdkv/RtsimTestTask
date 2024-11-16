namespace RtsimTestTask.Api.Requests.Account;

public record LoginUserRequest(
    string Username,
    string Password
);