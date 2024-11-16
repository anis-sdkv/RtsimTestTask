using AutoMapper;
using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.DataMappers;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<CreateOrganizationDto, OrganizationEntity>(MemberList.Source);
        CreateMap<RegisterUserDto, UserEntity>(MemberList.Source)
            .ForSourceMember(src => src.Password, opt => opt.DoNotValidate());


        CreateMap<OrganizationEntity, DomainOrganization>(MemberList.Destination)
            .ReverseMap();

        CreateMap<UserEntity, DomainUser>(MemberList.Destination)
            .ReverseMap();
    }
}