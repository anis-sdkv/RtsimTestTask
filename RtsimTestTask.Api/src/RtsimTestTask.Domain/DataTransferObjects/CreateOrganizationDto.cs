namespace RtsimTestTask.Domain.DataTransferObjects;

public record CreateOrganizationDto(
    string OrganizationName,
    string? Address = null,
    string? PhoneNumber = null);