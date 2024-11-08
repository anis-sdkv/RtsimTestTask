namespace RtsimTestTask.Api.Requests.Account;

public record LoginRequest(
    string Username,
    string Password
);