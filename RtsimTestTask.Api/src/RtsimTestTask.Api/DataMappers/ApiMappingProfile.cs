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
        CreateRequestMaps();
        CreateResponseMaps();
        CreateAccountModelMaps();
    }

    private void CreateAccountModelMaps()
    {
        CreateMap<LoginUserRequest, LoginUserDto>();
        CreateMap<RegisterUserRequest, RegisterUserDto>();
    }

    private void CreateRequestMaps()
    {
        CreateMap<CreateOrganizationRequest, CreateOrganizationDto>();
        CreateMap<UpdateOrganizationRequest, UpdateOrganizationDto>();

        CreateMap<SearchOrganizationsRequest, SearchOrganizationDto>();
        CreateMap<SearchOrganizationsRequest.QueryParameters, SearchOrganizationDto.QueryParameters>();
        CreateMap<SearchOrganizationsRequest.SortParameters, SearchOrganizationDto.SortParameters>();
        CreateMap<SearchOrganizationsRequest.DateFilterParameters, SearchOrganizationDto.DateFilterParameters>();
    }

    private void CreateResponseMaps()
    {
        CreateMap<DomainUser, UserResponse>();
        CreateMap<DomainOrganization, OrganizationResponse>();
    }
}