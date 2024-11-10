namespace RtsimTestTask.Domain.Exceptions;

public class UserExistException(string name) :
    DomainException($"A User with the username {name} already exists!"){}