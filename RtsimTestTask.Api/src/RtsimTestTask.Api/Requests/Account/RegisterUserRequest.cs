namespace RtsimTestTask.Api.Requests.Account;

public record RegisterUserRequest(
    string Username,
    string Password,
    Guid OrganizationId,
    string Position,
    string? Email,
    string? PhoneNumber
);