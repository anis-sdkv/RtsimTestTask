using Microsoft.AspNetCore.Mvc;
using RtsimTestTask.Api.DataMappers;
using RtsimTestTask.Api.Requests;
using RtsimTestTask.Api.Requests.Account;
using RtsimTestTask.Api.Responses;
using RtsimTestTask.Core.Abstractions.Authentication;

namespace RtsimTestTask.Api.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController(IAuthenticationProvider authenticationProvider) : ControllerBase
{
    [HttpPost("register")]
    public async Task<OperationResultResponse> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result =
            await authenticationProvider.RegisterAsync(request.ToDomainUser(), request.Password, cancellationToken);
        return result.MapToResponse();
    }

    [HttpPost("login")]
    public async Task<OperationResultResponse<Guid>> Register(LoginRequest request, CancellationToken cancellationToken)
    {
        // var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email) };
        // ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        // await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        throw new NotImplementedException();
    }

    [HttpGet("logout")]
    public async Task<OperationResultResponse> Logout(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}