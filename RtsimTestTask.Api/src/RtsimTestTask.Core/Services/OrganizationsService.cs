using RtsimTestTask.Core.Abstractions.Repositories;
using RtsimTestTask.Core.Abstractions.Services;
using RtsimTestTask.Core.DataTransferObjects;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Core.Exceptions;

namespace RtsimTestTask.Core.Services;

public class OrganizationsService(IUnitOfWork unitOfWork) : IOrganizationsService
{
    public Task CreateOrganizationAsync(
        Organization organization,
        CancellationToken cancellationToken)
    {
        unitOfWork.OrganizationRepository.Add(organization, cancellationToken);
        return unitOfWork.SaveChangesAsync(cancellationToken);
    }


    public Task<Organization> GetOrganizationByIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken)
    {
        return unitOfWork.OrganizationRepository.GetAsync(organizationId, cancellationToken);
    }

    public Task<IEnumerable<Organization>> SearchOrganizationsAsync(
        SearchOrganizationDto searchParams,
        CancellationToken cancellationToken)
    {
        return unitOfWork.OrganizationRepository.Search(searchParams, cancellationToken);
    }


    public Task<IEnumerable<User>> GetUsersByOrganizationIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken)
    {
        return unitOfWork.UserRepository.GetUsersByOrganization(organizationId, cancellationToken);
    }

    public async Task UpdateOrganizationAsync(
        Organization organization,
        CancellationToken cancellationToken)
    {
        await unitOfWork.OrganizationRepository.UpdateAsync(organization, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }


    public async Task DeleteOrganizationAsync(
        Guid organizationId,
        CancellationToken cancellationToken)
    {
        await unitOfWork.OrganizationRepository.RemoveAsync(organizationId, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}