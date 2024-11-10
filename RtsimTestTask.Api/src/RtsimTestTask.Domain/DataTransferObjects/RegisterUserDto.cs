namespace RtsimTestTask.Domain.DataTransferObjects;

public record RegisterUserDto(
    string UserName,
    string Password,
    Guid OrganizationId,
    string Position,
    string? Email = null,
    string? PhoneNumber = null
);