using RtsimTestTask.Core.Abstractions.Repositories;
using RtsimTestTask.Core.Abstractions.Services;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Services;

public class OrganizationsService(IUnitOfWork unitOfWork) : IOrganizationsService
{
    public Task CreateOrganizationAsync(
        DomainOrganization domainOrganization,
        CancellationToken cancellationToken)
    {
        unitOfWork.OrganizationRepository.Add(domainOrganization, cancellationToken);
        return unitOfWork.SaveChangesAsync(cancellationToken);
    }


    public Task<DomainOrganization> GetOrganizationByIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken)
    {
        return unitOfWork.OrganizationRepository.GetAsync(organizationId, cancellationToken);
    }

    public Task<IEnumerable<DomainOrganization>> SearchOrganizationsAsync(
        SearchOrganizationDto searchParams,
        CancellationToken cancellationToken)
    {
        return unitOfWork.OrganizationRepository.Search(searchParams, cancellationToken);
    }


    public Task<IEnumerable<DomainUser>> GetUsersByOrganizationIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken)
    {
        return unitOfWork.UserRepository.GetUsersByOrganization(organizationId, cancellationToken);
    }

    public async Task UpdateOrganizationAsync(
        DomainOrganization domainOrganization,
        CancellationToken cancellationToken)
    {
        await unitOfWork.OrganizationRepository.UpdateAsync(domainOrganization, cancellationToken);
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