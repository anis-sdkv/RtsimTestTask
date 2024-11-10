namespace RtsimTestTask.Domain.DomainEntities;

public abstract record DomainEntity<TKey>(TKey Id, DateTime CreatedAt);