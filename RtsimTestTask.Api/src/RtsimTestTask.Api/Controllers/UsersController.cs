using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RtsimTestTask.Api.Responses;
using RtsimTestTask.Domain.Abstractions.Services;

namespace RtsimTestTask.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController(IUsersService service, IMapper mapper)
{
    [HttpGet("{id:guid}")]
    public async Task<UserResponse> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync(id, cancellationToken);
        return mapper.Map<UserResponse>(result);
    }

    [HttpGet]
    public async Task<IEnumerable<UserResponse>> GetAllUsers(CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<UserResponse>>(result);
    }
}