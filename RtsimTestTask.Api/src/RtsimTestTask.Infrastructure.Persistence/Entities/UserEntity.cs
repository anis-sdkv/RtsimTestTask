using Microsoft.AspNetCore.Identity;

namespace RtsimTestTask.Infrastructure.Persistence.Entities;

public class UserEntity : IdentityUser
{
    public string Position { get; set; }
    public Guid OrganizationId { get; set; }
    public DateTime? CreatedAt { get; set; }
}