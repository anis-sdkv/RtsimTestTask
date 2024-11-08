namespace RtsimTestTask.Api.Requests;

public record RegisterRequest(
    string Username,
    string Password,
    Guid OrganizationId,
    string Position,
    string? Email,
    string? PhoneNumber
);