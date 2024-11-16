using Microsoft.AspNetCore.Identity;

namespace RtsimTestTask.Infrastructure.Persistence.Entities;

public class Role : IdentityRole
{
    public Role()
    {
    }

    public Role(string roleName)
    {
        base.Name = roleName;
        base.NormalizedName = roleName.ToUpperInvariant();
    }
}