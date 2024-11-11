using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RtsimTestTask.Domain.Abstractions.Authentication;
using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.Abstractions.Services;
using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.DomainEntities;
using RtsimTestTask.Domain.Exceptions;
using RtsimTestTask.Infrastructure.Persistence.DataMappers;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.Authentication;

public class AccountManager(
    UserManager<UserEntity> userManager,
    IJwtProvider jwtProvider,
    ApplicationDbContext context,
    IOrganizationsRepository organizationsRepository,
    IMapper mapper
) : IAccountManager
{
    public async Task<string> LoginAsync(LoginUserDto loginData, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByNameAsync(loginData.Username);
        if (user is null) throw new UserNotFoundException(loginData.Username);

        var passwordCorrect = await userManager.CheckPasswordAsync(user, loginData.Password);
        if (!passwordCorrect) throw new LoginException();

        var roles = await userManager.GetRolesAsync(user);
        return jwtProvider.GenerateToken(user.Id, roles);
    }

    public async Task<Guid> RegisterAsync(
        RegisterUserDto registerData,
        string role,
        CancellationToken cancellationToken)
    {
        var userEntity = mapper.Map<UserEntity>(registerData);
        var user = await userManager.FindByNameAsync(registerData.UserName);
        if (user is not null) throw new UserAlreadyExistException(registerData.UserName);

        var organization = await organizationsRepository.GetByIdAsync(registerData.OrganizationId, cancellationToken);
        if (organization is null) throw new OrganizationNotExistException(registerData.OrganizationId);

        await using (var transaction = await context.Database.BeginTransactionAsync(cancellationToken))
        {
            var result = await userManager.CreateAsync(userEntity, registerData.Password);
            if (!result.Succeeded)
                throw new RegistrationException(result.Errors.Select(x => x.Description));
            var roleResult = await userManager.AddToRoleAsync(userEntity, role);
            if (!roleResult.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new TransactionAbortedException();
            }

            await transaction.CommitAsync(cancellationToken);
        }

        return Guid.Parse(userEntity.Id);
    }

    public async Task UpdateAccountData(DomainUser userNewData, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}