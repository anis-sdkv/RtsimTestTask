namespace RtsimTestTask.Api.Responses;

public record UserResponse(
    Guid Id,
    DateTime CreatedAt,
    string Username,
    string Position,
    Guid OrganizationId,
    string? Email = null,
    string? PhoneNumber = null
);