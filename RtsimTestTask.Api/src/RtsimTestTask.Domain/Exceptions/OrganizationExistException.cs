namespace RtsimTestTask.Domain.Exceptions;

public class OrganizationExistException(string name) :
    DomainException($"An organization with the name {name} already exists!"){}