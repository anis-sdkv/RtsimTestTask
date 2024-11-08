using Microsoft.AspNetCore.Mvc;
using RtsimTestTask.Api.DataMappers;
using RtsimTestTask.Api.Requests.Organizations;
using RtsimTestTask.Api.Responses;
using RtsimTestTask.Core.Abstractions.Authentication;
using RtsimTestTask.Core.Abstractions.Services;

namespace RtsimTestTask.Api.Controllers;

[ApiController]
[Route("organizations")]
public class OrganizationsController(IOrganizationsService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrganizationById(int id)
    {
        service.
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> GetOrganizations(SearchOrganizationsRequest id)
    {
        throw new NotImplementedException();
    }

    [HttpPost()]
    public async Task<IActionResult> CreateOrganization()
    {
        // Логика для создания новой организации
    }

    public async Task<IActionResult> DeleteOrganization(int id)
    {
        service.DeleteOrganizationAsync();
        throw new NotImplementedException();
    }
}