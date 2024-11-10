namespace RtsimTestTask.Api.Requests.Account;

public record UpdateProfileRequest(
    string Username,
    string Position,
    string? Email,
    string? PhoneNumber
);