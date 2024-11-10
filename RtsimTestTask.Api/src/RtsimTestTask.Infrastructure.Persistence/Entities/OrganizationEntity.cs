using RtsimTestTask.Domain;
using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.Infrastructure.Persistence.Entities;

public class OrganizationEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string OrganizationName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<UserEntity> Employees { get; set; }
}