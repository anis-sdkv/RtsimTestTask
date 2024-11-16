namespace RtsimTestTask.Domain.Exceptions;

public class UserAlreadyExistException(string name) :
    DomainException($"A User with the username {name} already exists!"){}