namespace RtsimTestTask.Domain.Exceptions;

public class UserNotFoundException : DomainException
{
    public UserNotFoundException(string username) : base($"User with name {username} not found in the system.")
    {
        
    }
    public UserNotFoundException(Guid id) : base($"User with id {id} not found in the system.")
    {
        
    }
}
