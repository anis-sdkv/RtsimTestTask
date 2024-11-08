using Microsoft.AspNetCore.Mvc;
using RtsimTestTask.Api.Requests;
using RtsimTestTask.Api.Requests.Account;
using RtsimTestTask.Api.Responses;
using RtsimTestTask.Core.Abstractions.Authentication;

namespace RtsimTestTask.Api.Controllers;

[ApiController]
[Route("/account")]
public class AccountsController(IAuthenticationProvider authenticationProvider) : ControllerBase
{
    [HttpPost("register")]
    public async Task<OperationResultResponse> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPost("login")]
    public async Task<OperationResultResponse<Guid>> Register(LoginRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("logout")]
    public async Task<OperationResultResponse> Logout(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}