using Microsoft.AspNetCore.Identity;

namespace RtsimTestTask.Infrastructure.Persistence.Entities;

public class UserEntity : IdentityUser
{
    public DateTime? CreatedAt { get; set; }
    public string Position { get; set; }
    public Guid OrganizationId { get; set; }
    public OrganizationEntity Organization { get; set; }
}