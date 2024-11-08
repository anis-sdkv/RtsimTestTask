namespace RtsimTestTask.Core.DomainEntities;

public record User(
    Guid Id,
    DateTime CreatedAt,    
    string Username,
    string Position,
    Guid OrganizationId,
    string? Email = null,
    string? PhoneNumber = null
) : DomainEntity<Guid>(Id, CreatedAt);