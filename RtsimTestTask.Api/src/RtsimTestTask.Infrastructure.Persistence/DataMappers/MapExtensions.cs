using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.DataMappers;

public static class MapExtensions
{
    public static UserEntity MapToEntity(this DomainUser domainDomainUser) => new UserEntity
    {
        UserName = domainDomainUser.Username,
        Position = domainDomainUser.Position,
        OrganizationId = domainDomainUser.OrganizationId,
        Email = domainDomainUser.Email,
        PhoneNumber = domainDomainUser.PhoneNumber,
    };

    // public static DomainUser MapToDomain(this UserEntity entity) => new(
    //     Guid.Parse(entity.Id),
    //     entity.CreatedAt ??
    //     throw new ArgumentNullException(nameof(entity.UserName),
    //         $"Entity must contain a non-null {nameof(entity.UserName)}."),
    //     entity.UserName ??
    //     throw new ArgumentNullException(nameof(entity.UserName),
    //         $"Entity must contain a non-null {nameof(entity.UserName)}."),
    //     entity.Position,
    //     entity.Organization,
    //     entity.Email,
    //     entity.PhoneNumber
    // );
}