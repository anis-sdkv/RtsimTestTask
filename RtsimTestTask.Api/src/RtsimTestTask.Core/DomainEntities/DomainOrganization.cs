namespace RtsimTestTask.Core.DomainEntities;

public record DomainOrganization(
    Guid Id,
    DateTime CreatedAt,
    string OrganizationName,
    string Address,
    string PhoneNumber,
    Guid OwnerId,
    ICollection<DomainUser> Employees
) : DomainEntity<Guid>(Id, CreatedAt);