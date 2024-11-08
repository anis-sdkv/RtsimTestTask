using RtsimTestTask.Core;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Infrastructure.Persistence.Entities;

public class OrganizationEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string OrganizationName { get; set; }
    public string Address { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid? OwnerId { get; set; }
    public ICollection<UserEntity> Employees { get; set; }
}