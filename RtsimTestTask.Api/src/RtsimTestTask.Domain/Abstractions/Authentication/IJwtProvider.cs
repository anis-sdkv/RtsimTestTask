using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.Domain.Abstractions.Authentication;

public interface IJwtProvider
{
    string GenerateToken(string id, IEnumerable<string> roles);
}