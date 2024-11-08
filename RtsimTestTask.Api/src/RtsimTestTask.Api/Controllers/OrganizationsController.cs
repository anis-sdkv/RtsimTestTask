using Microsoft.AspNetCore.Mvc;
using RtsimTestTask.Api.Requests.Organizations;
using RtsimTestTask.Core.Abstractions.Services;

namespace RtsimTestTask.Api.Controllers;

[ApiController]
[Route("organizations")]
public class OrganizationsController(IOrganizationsService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrganizationById(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> GetOrganizations(SearchOrganizationsRequest id)
    {
        throw new NotImplementedException();
    }

    // [HttpPost()]
    // public async Task<IActionResult> CreateOrganization()
    // {
    //     throw new NotImplementedException();
    // }

    // public async Task<IActionResult> DeleteOrganization(int id)
    // {
    //     throw new NotImplementedException();
    // }
}