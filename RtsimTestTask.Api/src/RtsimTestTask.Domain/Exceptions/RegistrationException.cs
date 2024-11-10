namespace RtsimTestTask.Domain.Exceptions;

public class RegistrationException(IEnumerable<string> errors) : DomainException("An error occurred during registration.")
{
    public IEnumerable<string> Errors { get; } = errors;
}