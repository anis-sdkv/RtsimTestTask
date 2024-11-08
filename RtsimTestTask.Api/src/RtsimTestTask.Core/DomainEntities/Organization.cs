namespace RtsimTestTask.Core.DomainEntities;

public record Organization(
    Guid Id,
    DateTime CreatedAt,
    string OrganizationName,
    string Address,
    string PhoneNumber,
    Guid OwnerId,
    ICollection<User> Employees
) : DomainEntity<Guid>(Id, CreatedAt);