using AutoMapper;
using RtsimTestTask.Api.Requests.Account;
using RtsimTestTask.Api.Requests.Organizations;
using RtsimTestTask.Api.Responses;
using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.Api.DataMappers;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateOrganizationModelMaps();
        CreateAccountModelMaps();

        CreateMap<DomainUser, UserResponse>();
    }

    private void CreateAccountModelMaps()
    {
        CreateMap<LoginUserRequest, LoginUserDto>();
        CreateMap<RegisterUserRequest, RegisterUserDto>();
    }

    private void CreateOrganizationModelMaps()
    {
        CreateMap<CreateOrganizationRequest, CreateOrganizationDto>();

        CreateMap<UpdateOrganizationRequest, UpdateOrganizationDto>();

        CreateMap<SearchOrganizationsRequest, SearchOrganizationDto>();
        CreateMap<SearchOrganizationsRequest.QueryParameters, SearchOrganizationDto.QueryParameters>();
        CreateMap<SearchOrganizationsRequest.SortParameters, SearchOrganizationDto.SortParameters>();
        CreateMap<SearchOrganizationsRequest.DateFilterParameters, SearchOrganizationDto.DateFilterParameters>();
    }
}