using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace RtsimTestTask.Infrastructure.Persistence.Options;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public int ExpiresHours { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new(Encoding.UTF8.GetBytes(Key));

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudiences = [Audience],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = GetSymmetricSecurityKey(),
            ValidateLifetime = true,
        };
    }
}