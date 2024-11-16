namespace RtsimTestTask.Domain.Exceptions;

public class EntityNotFoundException(Guid id)
    : DomainException($"Entity with id {id} not found in the database.");