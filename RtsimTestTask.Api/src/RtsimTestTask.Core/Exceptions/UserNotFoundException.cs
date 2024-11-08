namespace RtsimTestTask.Core.Exceptions;

public class UserNotFoundException(string username) : DomainException("$User with name {name} not found in the system.");