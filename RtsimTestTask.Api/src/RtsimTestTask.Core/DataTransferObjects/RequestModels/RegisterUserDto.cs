namespace RtsimTestTask.Core.DataTransferObjects.RequestModels;

public record RegisterUserDto(
    string Username,
    string Password,
    Guid OrganizationId,
    string Position,
    string? Email,
    string? PhoneNumber
);