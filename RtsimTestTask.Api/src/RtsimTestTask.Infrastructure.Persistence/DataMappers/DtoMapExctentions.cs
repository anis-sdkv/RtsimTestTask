using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.DataMappers;

public static class DtoMapExctentions
{
    public static UserEntity CreateUser(this RegisterUserDto regData) => new UserEntity()
    {
        UserName = regData.Username,
        OrganizationId = regData.OrganizationId,
        Position = regData.Position,
        Email = regData.Email,
        PhoneNumber = regData.PhoneNumber,
    };
}