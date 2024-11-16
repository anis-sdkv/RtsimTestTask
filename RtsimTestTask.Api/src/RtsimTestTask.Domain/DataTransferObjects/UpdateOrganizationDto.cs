namespace RtsimTestTask.Domain.DataTransferObjects;

public record UpdateOrganizationDto(
    Guid Id,
    string OrganizationName,
    string? Address,
    string? PhoneNumber
);