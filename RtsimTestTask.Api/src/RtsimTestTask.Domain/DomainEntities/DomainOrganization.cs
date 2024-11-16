namespace RtsimTestTask.Domain.DomainEntities;

public record DomainOrganization(
    Guid Id,
    DateTime CreatedAt,
    string OrganizationName,
    string? Address,
    string? PhoneNumber
) : DomainEntity<Guid>(Id, CreatedAt);