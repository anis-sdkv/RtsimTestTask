namespace RtsimTestTask.Domain.DataTransferObjects;

public record RegisterUserDto(
    string UserName,
    string Password,
    Guid OrganizationId,
    string? Position = null,
    string? Email = null,
    string? PhoneNumber = null
);