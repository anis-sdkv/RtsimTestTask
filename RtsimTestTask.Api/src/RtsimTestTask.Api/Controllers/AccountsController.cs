using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RtsimTestTask.Api.Requests;
using RtsimTestTask.Api.Requests.Account;
using RtsimTestTask.Api.Responses;
using RtsimTestTask.Domain.Abstractions.Authentication;
using RtsimTestTask.Domain.Abstractions.Services;
using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.Roles;

namespace RtsimTestTask.Api.Controllers;

[ApiController]
[Route("/account")]
public class AccountsController(IAccountManager accountManager, IMapper mapper) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IResult> Register(RegisterUserRequest userRequest, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<RegisterUserDto>(userRequest);
        var createdId = await accountManager.RegisterAsync(dto, DomainRoles.User, cancellationToken);
        return Results.Ok(createdId);
    }

    [HttpPost("login")]
    public async Task<IResult> Login(LoginUserRequest userRequest, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<LoginUserDto>(userRequest);
        var token = await accountManager.LoginAsync(dto, cancellationToken);
        return Results.Ok(token);
    }
}