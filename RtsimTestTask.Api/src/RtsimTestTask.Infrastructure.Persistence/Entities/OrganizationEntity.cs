using RtsimTestTask.Core;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Infrastructure.Persistence.Entities;
public class OrganizationEntity
{
    public DateTime CreatedAt { get; set; }
    public string OrganizationName { get; set; }
    public string Address { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid? OwnerId { get; set; }
    public ICollection<UserEntity> Employees { get; set; }
}