using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RtsimTestTask.Domain.Abstractions.Authentication;
using RtsimTestTask.Domain.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.Options;

namespace RtsimTestTask.Infrastructure.Persistence.Authentication;

public class JwtProvider(IOptions<JwtOptions> authOptions) : IJwtProvider
{
    public string GenerateToken(string id, IEnumerable<string> roles)
    {
        var options = authOptions.Value;
        var claims = new[] { new Claim("id", id) }
            .Concat(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var credentials = new SigningCredentials(
            options.GetSymmetricSecurityKey(),
            SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            options.Issuer,
            options.Audience,
            claims,
            null,
            DateTime.Now.Add(TimeSpan.FromDays(options.ExpiresHours)),
            credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token) ?? throw new InvalidOperationException();
    }
}