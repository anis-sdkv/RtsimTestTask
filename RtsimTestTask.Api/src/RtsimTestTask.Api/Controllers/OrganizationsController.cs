using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RtsimTestTask.Api.DataMappers;
using RtsimTestTask.Api.Requests.Organizations;
using RtsimTestTask.Api.Responses;
using RtsimTestTask.Domain.Abstractions.Services;
using RtsimTestTask.Domain.Constants;
using RtsimTestTask.Domain.DataTransferObjects;

namespace RtsimTestTask.Api.Controllers;

[ApiController]
[Route("organizations")]
public class OrganizationsController(IOrganizationsService service, IMapper mapper) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<OrganizationResponse> GetOrganizationById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await service.GetOrganizationByIdAsync(id, cancellationToken);
        return mapper.Map<OrganizationResponse>(result);
    }

    [HttpGet]
    public async Task<IEnumerable<OrganizationResponse>> GetAllOrganizations(CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<OrganizationResponse>>(result);
    }

    [HttpPost("search")]
    public async Task<IEnumerable<OrganizationResponse>> SearchOrganizations(
        SearchOrganizationsRequest request,
        CancellationToken cancellationToken)
    {
        var dto = mapper.Map<SearchOrganizationDto>(request);
        var result = await service.SearchOrganizationsAsync(dto, cancellationToken);
        return mapper.Map<IEnumerable<OrganizationResponse>>(result);
    }

    [HttpGet("{id:guid}/users")]
    public async Task<IEnumerable<UserResponse>> GetUsersByOrganization(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await service.GetUsersByOrganizationIdAsync(id, cancellationToken);
        return mapper.Map<IEnumerable<UserResponse>>(result);
    }

    [HttpPost("create")]
    [Authorize(Roles = $"{DomainRoles.Admin}")]
    public async Task<Guid> CreateNewOrganization(
        CreateOrganizationRequest request,
        CancellationToken cancellationToken)
    {
        var dto = mapper.Map<CreateOrganizationDto>(request);
        return await service.CreateOrganizationAsync(dto, cancellationToken);
    }
}