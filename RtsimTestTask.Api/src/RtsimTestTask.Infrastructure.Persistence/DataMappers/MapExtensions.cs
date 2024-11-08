using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.DataMappers;

internal static class MapExtensions
{
    internal static UserEntity MapToEntity(this User domainUser) => new UserEntity
    {
        UserName = domainUser.Username,
        Position = domainUser.Position,
        OrganizationId = domainUser.OrganizationId,
        Email = domainUser.Email,
        PhoneNumber = domainUser.PhoneNumber,
    };

    internal static User MapToDomain(this UserEntity entity) => new (
        Guid.Parse(entity.Id),
        entity.CreatedAt ??
        throw new ArgumentNullException(nameof(entity.UserName),
            $"Entity must contain a non-null {nameof(entity.UserName)}."),
        entity.UserName ??
        throw new ArgumentNullException(nameof(entity.UserName),
            $"Entity must contain a non-null {nameof(entity.UserName)}."),
        entity.Position,
        entity.Organization,
        entity.Email,
        entity.PhoneNumber
    );
    
    internal static OrganizationEntity MapToEntity(this Organization domainOrganization) => new 
    {
        
    };
}