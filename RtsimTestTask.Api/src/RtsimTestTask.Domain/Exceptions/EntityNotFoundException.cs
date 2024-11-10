namespace RtsimTestTask.Domain.Exceptions;

public class EntityNotFoundException(string name) : DomainException($"Entity {name} not found in the database.");