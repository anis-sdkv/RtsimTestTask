using Microsoft.AspNetCore.Identity;

namespace RtsimTestTask.Infrastructure.Persistence.Options;

public class AppIdentityOptions
{
    public bool RequireDigit { get; set; } = false;
    public bool RequireLowercase { get; set; } = false;
    public bool RequireNonAlphanumeric { get; set; } = false;
    public bool RequireUppercase { get; set; } = false;
    public int RequiredLength { get; set; } = 1;
    public string AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    public void Configure(IdentityOptions options)
    {
        options.Password.RequireDigit = RequireDigit;
        options.Password.RequireLowercase = RequireLowercase;
        options.Password.RequireNonAlphanumeric = RequireNonAlphanumeric;
        options.Password.RequireUppercase = RequireUppercase;
        options.Password.RequiredLength = RequiredLength;

        options.User.AllowedUserNameCharacters = AllowedUserNameCharacters;
    }
}