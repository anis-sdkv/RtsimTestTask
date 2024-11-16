namespace RtsimTestTask.Domain.Exceptions;

public class RegistrationException(List<string> errors) : DomainException(
    "An error occurred during registration." +
    Environment.NewLine +
    string.Join(Environment.NewLine, errors)
)
{
    public IEnumerable<string> Errors { get; } = errors;
}