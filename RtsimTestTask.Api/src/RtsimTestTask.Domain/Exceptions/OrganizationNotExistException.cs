namespace RtsimTestTask.Domain.Exceptions;

public class OrganizationNotExistException(Guid organizationId): 
    DomainException($"An organization with id {organizationId} not found in the system!");